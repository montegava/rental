using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rental
{

    public enum EditMode
    {
        emNone,
        emAddNew,
        emEdit
    }

    public enum Dics
    {
        stations_dispach = 1,
        stations_distination = 2,
        clients = 3,
        shipnets = 4,
        tech_support = 5
    }

    /// <summary>
    /// Режим парсинга
    /// </summary>
    public enum ParcingMode
    {
        pmAll = 0,//--все сайты--
        pmCamelotFlat = 1,//Камелот
        pmCamelotHouse = 2,//Камелот
        pmMoyaReklama1 = 31,//Моя реклама
        pmMoyaReklama2 = 32,//Моя реклама
        pmMoyaReklama3 = 33,//Моя реклама
        pmIRR = 4,//Из рук в руки
        pmAvitoFlat = 5,//Avito - квартиры
        pmAvitoHouse = 6,//Avito - дома
        pmSlandoFlat = 7,//Slando - квартиры
        pmSlandoHouse = 8//Slando - дома
    }



    public enum ImageMode
    {
        imStar = 0,
        imCamelot = 1,
        imCamelotBlocked = 2,
        imMoyaReklama = 3,
        imMoyaReklamaBlocked = 4,
        imIRR = 5,
        imIRRBlocked = 6,
        imAvito = 7,
        imAvitoBlocked = 8,
        imSlando = 9,
        imSlandoBlocked = 10
    }
}
