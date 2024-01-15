namespace OnlineShop.DB.Models
{
    public class RolesRepository : IRolesRepository
    {
        private readonly DataBaseContext dataBaseContext;
        public RolesRepository(DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }

        public void AddRole(Role role)
        {
            dataBaseContext.Roles.Add(role);
            dataBaseContext.SaveChanges();
        }

        public void DeleteRole(Guid Id)
        {
            var role = this.GetRole(Id);
            dataBaseContext.Roles.Remove(role);
            dataBaseContext.SaveChanges();
        }

        public List<Role> GetAll()
        {
            return dataBaseContext.Roles.ToList();
        }

        public Role GetRole(Guid? Id)
        {
            var role = dataBaseContext.Roles.FirstOrDefault(x => x.Id == Id);
            return role;
        }
    }


    public interface IRolesRepository
    {
        public List<Role> GetAll();
        public void AddRole(Role role);
        public void DeleteRole(Guid Id);
        public Role GetRole(Guid? Id);

    }
}
