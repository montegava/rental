using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Управление общими сведениями о сборке осуществляется с помощью 
// набора атрибутов. Измените значения этих атрибутов, чтобы изменить сведения,
// связанные со сборкой.
[assembly: AssemblyTitle("Rental")]
[assembly: AssemblyDescription("renta")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Rental")]
[assembly: AssemblyCopyright("Copyright ©  2012")]
[assembly: AssemblyTrademark("montegava")]
[assembly: AssemblyCulture("")]

// Параметр ComVisible со значением FALSE делает типы в сборке невидимыми 
// для COM-компонентов.  Если требуется обратиться к типу в этой сборке через 
// COM, задайте атрибуту ComVisible значение TRUE для этого типа.
[assembly: ComVisible(false)]

// Следующий GUID служит для идентификации библиотеки типов, если этот проект будет видимым для COM
[assembly: Guid("f8130ce8-241d-4192-9bb1-c7a56710c4dd")]

// Сведения о версии сборки состоят из следующих четырех значений:
//
//      Основной номер версии
//      Дополнительный номер версии 
//      Номер построения
//      Редакция
//
// Можно задать все значения или принять номер построения и номер редакции по умолчанию, 
// используя "*", как показано ниже:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.*")]
[assembly: AssemblyFileVersion("1.1.0.1")]

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
[assembly: log4net.Config.Repository()]


