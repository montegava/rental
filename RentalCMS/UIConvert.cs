using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;


namespace RentalCMS
{
    /// <summary>
    /// this class is use to convert a data object to other data object
    /// </summary>
    public class UIConvert
    {
        /// <summary>
        /// Determines whether this instance [can be converted to double] the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        ///   <c>true</c> if this instance [can be converted to double] the specified text; otherwise, <c>false</c>.
        /// </returns>
        public static bool CanBeConvertedToPositiveDouble(Object text)
        {
            double value;
            return Double.TryParse(text.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out value) && value >= 0;
        }


        /// <summary>
        /// this function is use to increst an array list
        /// </summary>
        /// <param name="oldArray"></param>
        /// <param name="newSize"></param>
        /// <returns></returns>
        public static System.Array ResizeArray(System.Array oldArray, int newSize)
        {
            int oldSize = oldArray.Length;
            System.Type elementType = oldArray.GetType().GetElementType();
            System.Array newArray = System.Array.CreateInstance(elementType, newSize);

            int preserveLength = System.Math.Min(oldSize, newSize);

            if (preserveLength > 0)
                System.Array.Copy(oldArray, newArray, preserveLength);

            return newArray;
        }

        /// <summary>
        /// convert a list of string to a string
        /// </summary>
        /// <param name="selectedList"></param>
        /// <returns></returns>
        public static string ListOfStringToArray(List<string> selectedList)
        {
            if (selectedList == null || selectedList.Count == 0)
                return string.Empty;

            string finalString = string.Empty;
            foreach (string tempString in selectedList)
                finalString = string.IsNullOrEmpty(finalString) ? "" : "," + tempString;
            return finalString;
        }

    
   


        /// <summary>
        /// this function convert an string to a array of string, we use splitter to split chars
        /// </summary>
        /// <param name="selectedString"></param>
        /// <param name="selectedSpliter"></param>
        /// <returns></returns>
        public static List<string> ConvertStringToListOfString(string selectedString, char selectedSpliter = ',')
        {
            // -- check if we have a string
            if (string.IsNullOrEmpty(selectedString))
                return null;

            string[] firstSplit = selectedString.Split(selectedSpliter);
            return firstSplit.Any() ? firstSplit.Select(tempSplit => tempSplit.Trim()).ToList() : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedStrings"></param>
        /// <param name="selectedSpliter"></param>
        /// <returns></returns>
        public static string ConvertListStringToString(List<string> selectedStrings, char selectedSpliter = ',')
        {
            // -- check if we have a string
            if (selectedStrings == null || selectedStrings.Count == 0)
                return null;

            string finalString = string.Empty;
            foreach (string tempString in selectedStrings)
                finalString += string.IsNullOrEmpty(finalString) ? tempString : selectedSpliter + tempString;

            return finalString;
        }



       
      

        #region "basic function use for convert"

        /// <summary>
        /// this function is use to convert an object to an float
        /// - we use this function to convert web request to float
        /// - we use to convert an combobox value to an float
        /// </summary>
        /// <param name="reqObject"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static float ToFloat(object reqObject, float errorValue)
        {
            float newValue;

            // -- convert object is null
            if (reqObject == null)
                return errorValue;
            // -- try to parse
            if (!float.TryParse(reqObject.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out newValue))
                return errorValue;
            return newValue;
        }

        /// <summary>
        /// this function is use to convert an object to an decimal
        /// </summary>
        /// <param name="reqObject"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static decimal ToDecimal(object reqObject, decimal errorValue)
        {
            decimal newValue;

            // -- convert object is null
            if (reqObject == null)
                return errorValue;
            // -- try to parse
            if (!decimal.TryParse(reqObject.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out newValue))
                return errorValue;
            return newValue;
        }

        /// <summary>
        /// this function is use to convert an object to an int
        /// - we use this function to convert web request to int
        /// - we use to convert an combobox value to an int
        /// </summary>
        /// <param name="reqObject"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static int ToInt32(object reqObject, int errorValue)
        {
            int newValue;

            // -- convert object is null
            if (reqObject == null)
                return errorValue;

            // -- try to parse
            if (!int.TryParse(reqObject.ToString(), out newValue))
                return errorValue;
            return newValue;
        }

        /// <summary>
        /// this function is use to convert an object to an int
        /// - we use this function to convert web request to int
        /// - we use to convert an combobox value to an int
        /// </summary>
        /// <param name="reqObject"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static short ToInt16(object reqObject, short errorValue)
        {
            short newValue;

            // -- convert object is null
            if (reqObject == null)
                return errorValue;

            // -- try to parse
            if (!short.TryParse(reqObject.ToString(), out newValue))
                return errorValue;
            return newValue;
        }

        /// <summary>
        /// this function is use to convert an object to an int
        /// - we use this function to convert web request to int
        /// - we use to convert an combobox value to an int
        /// </summary>
        /// <param name="reqObject"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static byte ToByte(object reqObject, byte errorValue)
        {
            byte newValue;

            // -- convert object is null
            if (reqObject == null)
                return errorValue;

            // -- try to parse
            if (!byte.TryParse(reqObject.ToString(), out newValue))
                return errorValue;
            return newValue;
        }

        /// <summary>
        /// this function try to convert an object to a bool
        /// - if object is null will return false
        /// </summary>
        /// <param name="reqObject"></param>
        /// <returns></returns>
        public static bool ToBool(object reqObject, bool errorValue)
        {
            // -- convert object is null
            if (reqObject == null)
                return errorValue;

            // -- try to parse
            try
            {
                return Convert.ToBoolean(reqObject);
            }
            catch (Exception)
            {
                return errorValue;
            }
        }

        /// <summary>
        /// this function try to convert an object to a string
        /// - if object is null will return an empty string
        /// </summary>
        /// <param name="reqObject"></param>
        /// <returns></returns>
        public static string ToString(object reqObject)
        {
            // -- convert object is null
            if (reqObject == null)
                return string.Empty;

            // -- try to parse
            return reqObject.ToString();
        }

        /// <summary>
        /// this function try to convert an object to a string
        /// - if object is null will return: errorValue
        /// </summary>
        /// <param name="reqObject"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static string ToString(object reqObject, string errorValue)
        {
            // -- convert object is null
            if (reqObject == null)
                return errorValue;

            // -- try to parse
            return reqObject.ToString();
        }


        //  -- data time format
        public static string DATE_TIME_FORMAT = "dd.MM.yyyy";
        public static string TIME_FORMAT = "H:mm:ss";

        /// <summary>
        /// this function is use to convert an object to data time
        /// - if object can't be convert will return DateTime.MinValue
        /// </summary>
        /// <param name="reqObject"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(object reqObject)
        {
            // -- convert object is null
            if (reqObject == null || string.IsNullOrEmpty(reqObject.ToString()))
                return DateTime.MinValue;
            DateTime date;
            return DateTime.TryParseExact(reqObject.ToString(), DATE_TIME_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None, out date) ? date : DateTime.MinValue;
        }

        #endregion

    


     
    }
}