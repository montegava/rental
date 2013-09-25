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
        All = 0,//--все сайты--
        
        CamelotFlat = 1,//Камелот квартиры
        CamelotHouse = 2,//Камелот дома

        MoyaReklamaFlat = 31,//Моя реклама снять_квартиру
        MoyaReklamaRoom = 32,//Моя реклама снять_комнату
        MoyaReklamaHouse = 33,//Моя реклама снять_дом

        IRR = 4,//Из рук в руки

        AvitoFlat = 5,//Avito - квартиры
        pmAvitoHouse = 6,//Avito - дома

        SlandoFlat = 7,//Slando - квартиры
        SlandoHouse = 8//Slando - дома
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
