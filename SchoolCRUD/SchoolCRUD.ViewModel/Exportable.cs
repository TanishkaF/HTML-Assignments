using System;


namespace SchoolCRUD.ViewModel
{
        [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
        public class ExportableAttribute : Attribute
        {
            public bool IsExportable { get; }

            public ExportableAttribute(bool isExportable)
            {
                IsExportable = isExportable;
            }
        }
}