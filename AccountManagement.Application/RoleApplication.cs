using _0_Framework.Application;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Application
{
    public class RoleApplication : IRoleApplication
    {
        private readonly IRoleRepository _roleRepository;

        public RoleApplication(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public OperationResult Create(CreateRole command)
        {
            var operation = new OperationResult();

            if (_roleRepository.Exists(x => x.Name == command.Name))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var role = new Role(command.Name);
            _roleRepository.Create(role);
            _roleRepository.SaveChanges();

            return operation.Succeeded();
        }

        public OperationResult Edit(EditRole command)
        {
            throw new NotImplementedException();
        }

        public EditRole GetDetails(int id)
        {
            throw new NotImplementedException();
        }

        public List<RoleViewModel> List()
        {
            throw new NotImplementedException();
        }
    }
}
