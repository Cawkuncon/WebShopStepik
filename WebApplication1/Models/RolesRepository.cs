namespace WebApplication1.Models
{
    public class RolesRepository : IRolesRepository
    {
        private List<Role> _roles = new List<Role>();

        public void AddRole(Role role)
        {
            _roles.Add(role);
        }

        public void DeleteRole(Role role)
        {
            _roles.Remove(role);
        }

        public List<Role> GetAll()
        {
            return _roles;
        }
    }


    public interface IRolesRepository
    {
        public List<Role> GetAll();
        public void AddRole(Role role);
        public void DeleteRole(Role role);

    }
}
