using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Application
{
    public class RoleApplication : IRoleApplication
    {
        private readonly IRoleRepository _roleRepository;

        public RoleApplication(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public OperationResult Create(CreateRole command, List<int> permissions)
        {
            var operation = new OperationResult();

            if (_roleRepository.Exists(x => x.Name == command.Name))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var role = new Role(command.Name);
            _roleRepository.Create(role);
            _roleRepository.SaveChanges();

            if (!_roleRepository.AddPermissionsToRole(role.Id, permissions))
            {
                return operation.Failed(ApplicationMessages.OperationFailed);
            }

            return operation.Succeeded();
        }

        public OperationResult Edit(EditRole command)
        {
            var operation = new OperationResult();

            var role = _roleRepository.GetById(command.Id);

            if (role == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            
            if (_roleRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }

            role.Edit(command.Name);
            _roleRepository.SaveChanges();

            return operation.Succeeded();
        }

        public EditRole GetDetails(int id)
        {
            return _roleRepository.GetDetails(id);
        }

        public List<RoleViewModel> List()
        {
            return _roleRepository.List();
        }

        public OperationResult Remove(int id)
        {
            var operation = new OperationResult();

            var role = _roleRepository.GetById(id);

            if (role == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            role.Remove();
            _roleRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Restore(int id)
        {
            var operation = new OperationResult();

            var role = _roleRepository.GetById(id);

            if (role == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            role.Restore();
            _roleRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<PermissionViewModel> GetAllPermissions()
        {
            return _roleRepository.GetAllPermissions();
        }

        public List<int> SelectedPermissionsRole(int id)
        {
            return _roleRepository.SelectedPermissionsRole(id);
        }
    }
}
