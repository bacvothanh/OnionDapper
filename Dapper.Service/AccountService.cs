using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Data.Enums;
using Dapper.Data.Logic;
using Dapper.Data.Models;
using Dapper.Infrastructure;
using Dapper.Repository.Interfaces;
using Dapper.Service.Interfaces;
using Dapper.ViewModel;

namespace Dapper.Service
{
    public class AccountService : GenericService<Account>, IAccountService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository repository, IRoleRepository roleRepository) : base(repository)
        {
            _roleRepository = roleRepository;
            _accountRepository = repository;
        }

        public ApplicationResult<Account> Login(string username, string password)
        {
            var account = _accountRepository.Gets(new {Username = username})
                .FirstOrDefault();
            if (account == null)
                return ApplicationResult.Fail<Account>(ApplicationErrorCode.ErrorIsExist, "Account");
            var isValidPassword = account.IsValidPassword(password);
            if (isValidPassword == false)
                return ApplicationResult.Fail<Account>(ApplicationErrorCode.ErrorIsNotValid, "Password");
            if (account.Status != AccountStatus.Active)
                return ApplicationResult.Fail<Account>("The account does not active yet");

            var role = _roleRepository.GetByName(ApplicationConstant.Role.User);
            account.Roles = new List<Role> {role};
            return ApplicationResult.Ok(account);
        }
        

        public ApplicationResult ConfirmEmail(string token)
        {
            var account = _accountRepository.Gets(new {TokenConfrimEmail = token}).FirstOrDefault();
            if (account == null)
                return ApplicationResult.Fail(ApplicationErrorCode.ErrorIsNotValid, "Wrong token");
            account.Status =AccountStatus.Active;
            account.ConfirmEmail();
            _accountRepository.Update(account);
            return ApplicationResult.Ok();
        }
        
        public void Dispose()
        {
        }

        public ApplicationResult<Account> Create(RegisterBindingModel model)
        {
            var account = _accountRepository.Gets(new { Username = model.Email, IsDeleted = false }).FirstOrDefault();
            if (account != null)
                return ApplicationResult.Fail<Account>("The account already exists");
            account = new Account { UserName = model.Email, Email = model.Email, DisplayName = model.DisplayName };
            account.SetPassword(model.Password);
            account.GenerateActiveToken();

            var accountResult = _accountRepository.Insert(account);
            var insertRoleResult = _roleRepository.InsertRoleForAccount(accountResult.Id, ApplicationConstant.Role.User);
            if (insertRoleResult.IsFailure)
                return ApplicationResult.Fail<Account>(insertRoleResult.Errors);
            return ApplicationResult.Ok(account);
        }


        public Task CreateAsync(Account user)
        {
            _accountRepository.Insert(user);
            return Task.FromResult(0);
        }

        public Task UpdateAsync(Account user)
        {
            _accountRepository.Update(user);
            return Task.FromResult(0);
        }

        public Task DeleteAsync(Account user)
        {
            _accountRepository.DeleteById(user.Id);
            return Task.FromResult(0);
        }

        public Task<Account> FindByIdAsync(int userId)
        {
            var account = _accountRepository.GetById(userId);
            return Task.FromResult(account);
        }

        public Task<Account> FindByNameAsync(string userName)
        {
            var account = _accountRepository.Gets(new {UserName = userName}).FirstOrDefault();
            return Task.FromResult(account);
        }
      
    }
}
