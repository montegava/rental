using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;


namespace TowardsNext
{









    public static class ListConvertor
    {

        public static DataSet CreateDataSet<T>(List<T> list)
        {
            //list is nothing or has nothing, return nothing (or add exception handling)
            if (list == null || list.Count == 0) { return null; }

            //get the type of the first obj in the list
            var obj = list[0].GetType();

            //now grab all properties
            var properties = obj.GetProperties();

            //make sure the obj has properties, return nothing (or add exception handling)
            if (properties.Length == 0) { return null; }

            //it does so create the dataset and table
            var dataSet = new DataSet();
            var dataTable = new DataTable();

            //now build the columns from the properties
            var columns = new DataColumn[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                //TYPE_ID
                if (properties[i].Name != "TYPE_ID")
                    columns[i] = new DataColumn(properties[i].Name, properties[i].PropertyType);
            }

            //add columns to table
            dataTable.Columns.AddRange(columns);

            //now add the list values to the table
            foreach (var item in list)
            {
                //create a new row from table
                var dataRow = dataTable.NewRow();

                //now we have to iterate thru each property of the item and retrieve it's value for the corresponding row's cell
                var itemProperties = item.GetType().GetProperties();

                for (int i = 0; i < itemProperties.Length; i++)
                {
                    if (itemProperties[i].Name != "TYPE_ID")
                        dataRow[i] = itemProperties[i].GetValue(item, null);
                }

                //now add the populated row to the table
                dataTable.Rows.Add(dataRow);
            }

            //add table to dataset
            dataSet.Tables.Add(dataTable);

            //return dataset
            return dataSet;
        }

        public static DataSet ConvertToDataSet<T>(IList list)
        {
            DataSet dataSet = new DataSet();

            CreateDataSet(dataSet, typeof(T), false);

            FillDataSet(typeof(T), list, dataSet, -1);
            CreateRelations(dataSet, typeof(T), null);

            return dataSet;
        }

        /// 
        /// Create the structure for all the tables in the data set        
        /// 
        /// Data set in which tables will be created
        /// Type of which dataset has to be created
        /// Whether current type is a child table        
        private static void CreateDataSet(DataSet dataSet, Type type, bool isChildTable)
        {
            DataTable dataTable = new DataTable(type.Name);

            ////Create the ID columns for having relation in the tables 
            //dataTable.Columns.Add(new DataColumn("ID", typeof(int)));
            //if (isChildTable)
            //{
            //    dataTable.Columns.Add(new DataColumn("ParentID", typeof(int)));
            //}

            // Create the structure for the data tables to be
            // added in the the data set            
            foreach (PropertyInfo pInfo in type.GetProperties())
            {
                if (pInfo.PropertyType.IsGenericType &&
                (pInfo.PropertyType.GetGenericTypeDefinition() == typeof(List<>)
                || pInfo.PropertyType.GetGenericTypeDefinition() == typeof(IList<>)))
                {
                    // If associate lists are there make then another table
                    CreateDataSet(dataSet, pInfo.PropertyType.GetGenericArguments()[0], true);
                }
                else
                {
                    //TYPE_ID
                    if (pInfo.Name == "TYPE_ID")
                        continue;
                    dataTable.Columns.Add(new DataColumn(pInfo.Name, pInfo.PropertyType));
                }
            }

            //Add the table to the dataset
            dataSet.Tables.Add(dataTable);
        }

        /// 
        /// Fill all the tables of data set with data in the respective list        
        /// 
        /// Type of which datatable is to be filled
        /// List of data
        /// Data Set in which data tables will be filled with data
        /// ID of parent record. If -1 one then no parent        
        private static void FillDataSet(Type type, IList list, DataSet dataSet, int parentID)
        {
            PropertyInfo[] propertyInfos = type.GetProperties();
            DataTable dataTable = dataSet.Tables[type.Name];
            int id = dataTable.Rows.Count + 1;

            foreach (object item in list)
            {
                DataRow row = dataTable.NewRow();

                // Set new id and related parent id
                row["ID"] = id;
                if (parentID != -1)
                    row["ParentID"] = parentID;

                // Load all the data from the properties of the type
                // and save them into the datatable                
                foreach (PropertyInfo info in propertyInfos)
                {
                    if (info.PropertyType.IsGenericType &&
                    (info.PropertyType.GetGenericTypeDefinition() == typeof(List<>)
                    || info.PropertyType.GetGenericTypeDefinition() == typeof(IList<>)))
                    {
                        IList subList = (IList)info.GetValue(item, null);
                        if (subList != null && subList.Count > 0)
                        {
                            FillDataSet(subList[0].GetType(),
                            subList,
                            dataSet, id);
                        }
                    }
                    else
                    {
                        if (info.Name == "TYPE_ID")
                            continue;
                        row[info.Name] = info.GetValue(item, null);
                    }
                }

                dataTable.Rows.Add(row);
                id++;
            }
        }

        /// 
        /// Creates the relation between the tables according to the         
        /// type and parent table on field ID and ParentID        
        /// 
        /// Data set containing parent and child table
        /// Type of the list
        /// Parent table to which relations has to be done        
        private static void CreateRelations(DataSet dataSet, Type type, DataTable parentTable)
        {
            DataTable dataTable = dataSet.Tables[type.Name];

            // If parent table exsits then create relation
            // with child table on field Parent ID            
            if (parentTable != null)
            {
                dataSet.Relations.Add(
                new DataRelation(parentTable.TableName + "_ID_"
                                        + "PARENTID_" + dataTable.TableName,
                parentTable.Columns["ID"],
                dataTable.Columns["ParentID"]));
            }

            // Check for other lists under current object
            // go for another relation if exists            
            foreach (PropertyInfo pInfo in type.GetProperties())
            {
                if (pInfo.PropertyType.IsGenericType &&
                (pInfo.PropertyType.GetGenericTypeDefinition() == typeof(List<>)
                || pInfo.PropertyType.GetGenericTypeDefinition() == typeof(IList<>)))
                {
                    // If associate lists are there make then another table
                    CreateRelations(dataSet,
pInfo.PropertyType.GetGenericArguments()[0],
dataTable);
                }
            }
        }
    }
}
