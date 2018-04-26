using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Infrastructure
{
    public enum ApplicationErrorCode
    {
        #region model errors

        /// <summary>
        /// {0} is not exist
        /// </summary>
        [Description("{0} is not exist")]
        ErrorIsNotExist = 1000,

        /// <summary>
        /// {0} is exist
        /// </summary>
        [Description("{0} already exists")]
        ErrorIsExist = 1001,

        /// <summary>
        /// {0} is not valid
        /// </summary>
        [Description("{0} is not valid")]
        ErrorIsNotValid = 1002,

        /// <summary>
        /// {0} is required
        /// </summary>
        [Description("The field {0} is required")]
        ErrorIsRequired = 1003,

        /// <summary>
        /// Name is duplicated
        /// </summary>
        [Description("{0} is duplicated")]
        ErrorIsDuplicated = 1008,
        /// <summary>
        /// {0} is deleted
        /// </summary>
        [Description("{0} is deleted")]
        ErrorIsDeleted = 1010,
        /// <summary>
        /// {0} is empty
        /// </summary>
        [Description("{0} is empty")]
        ErrorIsEmpty = 1011,
        /// <summary>
        /// {0} is not active
        /// </summary>
        [Description("{0} is not active")]
        ErrorIsNotActive = 1012,
        #endregion

        #region token errors
        [Description("{0}")]
        ErrorFacebookExpiresToken = 6190
        #endregion
    }
}
