using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TorneosAdmin.Web.Models;

namespace TorneosAdmin.Web.Identidad
{
    public class CustomRoleStore :
        IRoleStore<AplicationRole>
    //IRoleClaimStore<AplicationRole> //No Implementado
    {
        private readonly ModelEntities _db;

        public CustomRoleStore(ModelEntities db)
        {
            _db = db;
        }

        #region IRoleStore<IdentityRole> Members
        public Task<IdentityResult> CreateAsync(AplicationRole role, CancellationToken cancellationToken)
        {
            try
            {
                if (cancellationToken != null)
                    cancellationToken.ThrowIfCancellationRequested();

                if (role == null)
                    throw new ArgumentNullException(nameof(role));

                var roleEntity = GetRoleEntity(role);

                _db.Roles.Add(roleEntity);
                _db.SaveChanges();

                return Task.FromResult(IdentityResult.Success);
            }
            catch (Exception ex)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Code = ex.Message, Description = ex.Message }));
            }
        }

        public Task<IdentityResult> DeleteAsync(AplicationRole role, CancellationToken cancellationToken)
        {
            try
            {
                if (cancellationToken != null)
                    cancellationToken.ThrowIfCancellationRequested();

                if (role == null)
                    throw new ArgumentNullException(nameof(role));

                var roleEntity = GetRoleEntity(role);

                _db.Roles.Remove(roleEntity);
                _db.SaveChanges();

                return Task.FromResult(IdentityResult.Success);
            }
            catch (Exception ex)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Code = ex.Message, Description = ex.Message }));
            }
        }

        public Task<AplicationRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrWhiteSpace(roleId))
                throw new ArgumentNullException(nameof(roleId));

            var roleEntity = _db.Roles.Find(Convert.ToInt32(roleId));

            return Task.FromResult(GetIdentityRole(roleEntity));
        }

        public Task<AplicationRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrWhiteSpace(normalizedRoleName))
                throw new ArgumentNullException(nameof(normalizedRoleName));

            var roleEntity = _db.Roles.First(x => x.Descripcion.ToUpper() == normalizedRoleName);

            return Task.FromResult(GetIdentityRole(roleEntity));
        }

        public Task<string> GetNormalizedRoleNameAsync(AplicationRole role, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            return Task.FromResult(role.NormalizedName);
        }

        public Task<string> GetRoleIdAsync(AplicationRole role, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            return Task.FromResult(role.Id.ToString());
        }

        public Task<string> GetRoleNameAsync(AplicationRole role, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            return Task.FromResult(role.Name);
        }

        public Task SetNormalizedRoleNameAsync(AplicationRole role, string normalizedName, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            role.NormalizedName = normalizedName;

            return Task.CompletedTask;
        }

        public Task SetRoleNameAsync(AplicationRole role, string roleName, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            role.Name = roleName;

            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(AplicationRole role, CancellationToken cancellationToken)
        {
            try
            {
                if (cancellationToken != null)
                    cancellationToken.ThrowIfCancellationRequested();

                if (role == null)
                    throw new ArgumentNullException(nameof(role));

                var roleEntity = GetRoleEntity(role);

                _db.Roles.Update(roleEntity);
                _db.SaveChanges();

                return Task.FromResult(IdentityResult.Success);
            }
            catch (Exception ex)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Code = ex.Message, Description = ex.Message }));
            }
        }

        public void Dispose()
        {
            // Lifetimes of dependencies are managed by the IoC container, so disposal here is unnecessary.
        }
        #endregion

        #region IRoleClaimStore<IdentityRole> Members
        //public Task<IList<Claim>> GetClaimsAsync(IdentityRole role, CancellationToken cancellationToken = default(CancellationToken))
        //{
        //    if (cancellationToken != null)
        //        cancellationToken.ThrowIfCancellationRequested();

        //    if (role == null)
        //        throw new ArgumentNullException(nameof(role));

        //    IList<Claim> result = _unitOfWork.RoleClaimRepository.FindByRoleId(role.Id).Select(x => new Claim(x.ClaimType, x.ClaimValue)).ToList();

        //    return Task.FromResult(result);
        //}

        //public Task AddClaimAsync(IdentityRole role, Claim claim, CancellationToken cancellationToken = default(CancellationToken))
        //{
        //    if (cancellationToken != null)
        //        cancellationToken.ThrowIfCancellationRequested();

        //    if (role == null)
        //        throw new ArgumentNullException(nameof(role));

        //    if (claim == null)
        //        throw new ArgumentNullException(nameof(claim));

        //    var roleClaimEntity = new RoleClaim
        //    {
        //        ClaimType = claim.Type,
        //        ClaimValue = claim.Value,
        //        RoleId = role.Id
        //    };

        //    _unitOfWork.RoleClaimRepository.Add(roleClaimEntity);
        //    _unitOfWork.Commit();

        //    return Task.CompletedTask;
        //}

        //public Task RemoveClaimAsync(IdentityRole role, Claim claim, CancellationToken cancellationToken = default(CancellationToken))
        //{
        //    if (cancellationToken != null)
        //        cancellationToken.ThrowIfCancellationRequested();

        //    if (role == null)
        //        throw new ArgumentNullException(nameof(role));

        //    if (claim == null)
        //        throw new ArgumentNullException(nameof(claim));

        //    var roleClaimEntity = _unitOfWork.RoleClaimRepository.FindByRoleId(role.Id)
        //        .SingleOrDefault(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);

        //    if (roleClaimEntity != null)
        //    {
        //        _unitOfWork.RoleClaimRepository.Remove(roleClaimEntity.Id);
        //        _unitOfWork.Commit();
        //    }

        //    return Task.CompletedTask;
        //}
        #endregion

        #region Private Methods
        private Roles GetRoleEntity(AplicationRole AplicationRole)
        {
            return AplicationRole == null
                ? default(Roles)
                : new Roles
                {
                    ID = AplicationRole.Id,
                    Descripcion = AplicationRole.Name,
                    Estado = AplicationRole.IsDelete
                };
        }

        private AplicationRole GetIdentityRole(Roles roles)
        {
            return roles == null
                ? default(AplicationRole)
                : new AplicationRole
                {
                    Id = roles.ID,
                    Name = roles.Descripcion,
                    IsDelete = roles.Estado
                };
        }
        #endregion
    }
}
