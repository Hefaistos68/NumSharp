﻿/*
This file was generated by template ../NDArray.Elementwise.tt
In case you want to do some changes do the following 

1 ) adapt the tt file
2 ) execute powershell file "GenerateCode.ps1" on root level

*/

using System;

namespace NumSharp.Backends
{
    public partial class DefaultEngine
    {
        public override NDArray Divide(in NDArray lhs, in NDArray rhs)
        {
            switch (lhs.GetTypeCode)
            {
#if _REGEN
	            %foreach supported_dtypes,supported_dtypes_lowercase%
	            case NPTypeCode.#1: return Divide#1(lhs, rhs);
	            %
	            default:
		            throw new NotSupportedException();
#else
                case NPTypeCode.Boolean: return DivideBoolean(lhs, rhs);
                case NPTypeCode.Byte: return DivideByte(lhs, rhs);
                case NPTypeCode.Int16: return DivideInt16(lhs, rhs);
                case NPTypeCode.UInt16: return DivideUInt16(lhs, rhs);
                case NPTypeCode.Int32: return DivideInt32(lhs, rhs);
                case NPTypeCode.UInt32: return DivideUInt32(lhs, rhs);
                case NPTypeCode.Int64: return DivideInt64(lhs, rhs);
                case NPTypeCode.UInt64: return DivideUInt64(lhs, rhs);
                case NPTypeCode.Char: return DivideChar(lhs, rhs);
                case NPTypeCode.Double: return DivideDouble(lhs, rhs);
                case NPTypeCode.Single: return DivideSingle(lhs, rhs);
                case NPTypeCode.Decimal: return DivideDecimal(lhs, rhs);
                default:
                    throw new NotSupportedException();
#endif
            }
        }
    }
}
