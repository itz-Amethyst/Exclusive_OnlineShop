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
            var operation = new OperationResult();

            var role = new Role(command.Name);

            if (role == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            
            if (_roleRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            role.Edit(command.Name);
            
            _roleRepository.Create(role);
            _roleRepository.SaveChanges();

            return operation.Succeeded();
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
