using System;

namespace TemplateService.Common.CustomAttributes
{
    public class ExcelCustomAttribute: Attribute
    {
        public string Name { get; set; }
        public int Order { get; set; } = 0;
        public ExcelCustomAttribute(string name)
        {
            Name = name;
        }

        public ExcelCustomAttribute(string name, int order) : this(name)
        {
            Order = order;
        }
    }
}
