namespace RealEstateManagement_MinhNTSE196686.Filter
{
    public class RequireRoleAttribute : Attribute
    {
        public int[] AllowedRoles { get; }

        public RequireRoleAttribute(params int[] allowedRoles)
        {
            AllowedRoles = allowedRoles;
        }
    }
}
