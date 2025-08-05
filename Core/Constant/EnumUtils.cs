using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Constant
{
    public class EnumUtils
    {
        public enum TransType
        {
            VOUCHER,
            USER,
            QRCODE,
            OTP,
            MARKET
        }
        public enum StatusType
        {
            Pending,
            Failed,
            Rejected,
            Approved,
        }
        public enum Type
        {
            Verification,
            Registration,
            Confirmation
        }
    }
}
