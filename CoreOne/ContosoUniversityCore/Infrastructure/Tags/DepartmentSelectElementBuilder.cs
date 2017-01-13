﻿namespace ContosoUniversityCore.Infrastructure.Tags
{
    using Domain;

    public class DepartmentSelectElementBuilder : EntitySelectElementBuilder<Department>
    {
        protected override int GetValue(Department instance)
        {
            return instance.Id;
        }

        protected override string GetDisplayValue(Department instance)
        {
            return instance.Name;
        }
    }
}