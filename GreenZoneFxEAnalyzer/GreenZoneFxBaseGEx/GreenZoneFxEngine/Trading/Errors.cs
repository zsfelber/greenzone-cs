using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreenZoneFxEngine.Trading
{
    class Errors
    {
        //+------------------------------------------------------------------+
        //|                                                     stderror.mqh |
        //|                 Copyright © 2004-2007, MetaQuotes Software Corp. |
        //|                                       http://www.metaquotes.net/ |
        //+------------------------------------------------------------------+
        //---- errors returned from trade server
        public const int ERR_NO_ERROR =                                0;
        public const int ERR_NO_RESULT =                               1;
        public const int ERR_COMMON_ERROR =                            2;
        public const int ERR_INVALID_TRADE_PARAMETERS =                3;
        public const int ERR_SERVER_BUSY =                             4;
        public const int ERR_OLD_VERSION =                             5;
        public const int ERR_NO_CONNECTION =                           6;
        public const int ERR_NOT_ENOUGH_RIGHTS =                       7;
        public const int ERR_TOO_FREQUENT_REQUESTS =                   8;
        public const int ERR_MALFUNCTIONAL_TRADE =                     9;
        public const int ERR_ACCOUNT_DISABLED =                       64;
        public const int ERR_INVALID_ACCOUNT =                        65;
        public const int ERR_TRADE_TIMEOUT =                         128;
        public const int ERR_INVALID_PRICE =                         129;
        public const int ERR_INVALID_STOPS =                         130;
        public const int ERR_INVALID_TRADE_VOLUME =                  131;
        public const int ERR_MARKET_CLOSED =                         132;
        public const int ERR_TRADE_DISABLED =                        133;
        public const int ERR_NOT_ENOUGH_MONEY =                      134;
        public const int ERR_PRICE_CHANGED =                         135;
        public const int ERR_OFF_QUOTES =                            136;
        public const int ERR_BROKER_BUSY =                           137;
        public const int ERR_REQUOTE =                               138;
        public const int ERR_ORDER_LOCKED =                          139;
        public const int ERR_LONG_POSITIONS_ONLY_ALLOWED =           140;
        public const int ERR_TOO_MANY_REQUESTS =                     141;
        public const int ERR_TRADE_MODIFY_DENIED =                   145;
        public const int ERR_TRADE_CONTEXT_BUSY =                    146;
        public const int ERR_TRADE_EXPIRATION_DENIED =               147;
        public const int ERR_TRADE_TOO_MANY_ORDERS =                 148;
        public const int ERR_TRADE_HEDGE_PROHIBITED =                149;
        public const int ERR_TRADE_PROHIBITED_BY_FIFO =              150;
        //---- mql4 run time errors
        public const int ERR_NO_MQLERROR =                          4000;
        public const int ERR_WRONG_FUNCTION_POINTER =               4001;
        public const int ERR_ARRAY_INDEX_OUT_OF_RANGE =             4002;
        public const int ERR_NO_MEMORY_FOR_CALL_STACK =             4003;
        public const int ERR_RECURSIVE_STACK_OVERFLOW =             4004;
        public const int ERR_NOT_ENOUGH_STACK_FOR_PARAM =           4005;
        public const int ERR_NO_MEMORY_FOR_PARAM_STRING =           4006;
        public const int ERR_NO_MEMORY_FOR_TEMP_STRING =            4007;
        public const int ERR_NOT_INITIALIZED_STRING =               4008;
        public const int ERR_NOT_INITIALIZED_ARRAYSTRING =          4009;
        public const int ERR_NO_MEMORY_FOR_ARRAYSTRING =            4010;
        public const int ERR_TOO_LONG_STRING =                      4011;
        public const int ERR_REMAINDER_FROM_ZERO_DIVIDE =           4012;
        public const int ERR_ZERO_DIVIDE =                          4013;
        public const int ERR_UNKNOWN_COMMAND =                      4014;
        public const int ERR_WRONG_JUMP =                           4015;
        public const int ERR_NOT_INITIALIZED_ARRAY =                4016;
        public const int ERR_DLL_CALLS_NOT_ALLOWED =                4017;
        public const int ERR_CANNOT_LOAD_LIBRARY =                  4018;
        public const int ERR_CANNOT_CALL_FUNCTION =                 4019;
        public const int ERR_EXTERNAL_CALLS_NOT_ALLOWED =           4020;
        public const int ERR_NO_MEMORY_FOR_RETURNED_STR =           4021;
        public const int ERR_SYSTEM_BUSY =                          4022;
        public const int ERR_INVALID_FUNCTION_PARAMSCNT =           4050;
        public const int ERR_INVALID_FUNCTION_PARAMVALUE =          4051;
        public const int ERR_STRING_FUNCTION_INTERNAL =             4052;
        public const int ERR_SOME_ARRAY_ERROR =                     4053;
        public const int ERR_INCORRECT_SERIESARRAY_USING =          4054;
        public const int ERR_CUSTOM_INDICATOR_ERROR =               4055;
        public const int ERR_INCOMPATIBLE_ARRAYS =                  4056;
        public const int ERR_GLOBAL_VARIABLES_PROCESSING =          4057;
        public const int ERR_GLOBAL_VARIABLE_NOT_FOUND =            4058;
        public const int ERR_FUNC_NOT_ALLOWED_IN_TESTING =          4059;
        public const int ERR_FUNCTION_NOT_CONFIRMED =               4060;
        public const int ERR_SEND_MAIL_ERROR =                      4061;
        public const int ERR_STRING_PARAMETER_EXPECTED =            4062;
        public const int ERR_INTEGER_PARAMETER_EXPECTED =           4063;
        public const int ERR_DOUBLE_PARAMETER_EXPECTED =            4064;
        public const int ERR_ARRAY_AS_PARAMETER_EXPECTED =          4065;
        public const int ERR_HISTORY_WILL_UPDATED =                 4066;
        public const int ERR_TRADE_ERROR =                          4067;
        public const int ERR_END_OF_FILE =                          4099;
        public const int ERR_SOME_FILE_ERROR =                      4100;
        public const int ERR_WRONG_FILE_NAME =                      4101;
        public const int ERR_TOO_MANY_OPENED_FILES =                4102;
        public const int ERR_CANNOT_OPEN_FILE =                     4103;
        public const int ERR_INCOMPATIBLE_FILEACCESS =              4104;
        public const int ERR_NO_ORDER_SELECTED =                    4105;
        public const int ERR_UNKNOWN_SYMBOL =                       4106;
        public const int ERR_INVALID_PRICE_PARAM =                  4107;
        public const int ERR_INVALID_TICKET =                       4108;
        public const int ERR_TRADE_NOT_ALLOWED =                    4109;
        public const int ERR_LONGS_NOT_ALLOWED =                    4110;
        public const int ERR_SHORTS_NOT_ALLOWED =                   4111;
        public const int ERR_OBJECT_ALREADY_EXISTS =                4200;
        public const int ERR_UNKNOWN_OBJECT_PROPERTY =              4201;
        public const int ERR_OBJECT_DOES_NOT_EXIST =                4202;
        public const int ERR_UNKNOWN_OBJECT_TYPE =                  4203;
        public const int ERR_NO_OBJECT_NAME =                       4204;
        public const int ERR_OBJECT_COORDINATES_ERROR =             4205;
        public const int ERR_NO_SPECIFIED_SUBWINDOW =               4206;
        public const int ERR_SOME_OBJECT_ERROR =                    4207;

        public static string ErrorDescription(int e)
        {
            string error_string;
            //----
            switch (e)
            {
                //---- codes returned from trade server
                case 0:
                case 1: error_string = "no error"; break;
                case 2: error_string = "common error"; break;
                case 3: error_string = "invalid trade parameters"; break;
                case 4: error_string = "trade server is busy"; break;
                case 5: error_string = "old version of the client terminal"; break;
                case 6: error_string = "no connection with trade server"; break;
                case 7: error_string = "not enough rights"; break;
                case 8: error_string = "too frequent requests"; break;
                case 9: error_string = "malfunctional trade operation (never returned error)"; break;
                case 64: error_string = "account disabled"; break;
                case 65: error_string = "invalid account"; break;
                case 128: error_string = "trade timeout"; break;
                case 129: error_string = "invalid price"; break;
                case 130: error_string = "invalid stops"; break;
                case 131: error_string = "invalid trade volume"; break;
                case 132: error_string = "market is closed"; break;
                case 133: error_string = "trade is disabled"; break;
                case 134: error_string = "not enough money"; break;
                case 135: error_string = "price changed"; break;
                case 136: error_string = "off quotes"; break;
                case 137: error_string = "broker is busy (never returned error)"; break;
                case 138: error_string = "requote"; break;
                case 139: error_string = "order is locked"; break;
                case 140: error_string = "long positions only allowed"; break;
                case 141: error_string = "too many requests"; break;
                case 145: error_string = "modification denied because order too close to market"; break;
                case 146: error_string = "trade context is busy"; break;
                case 147: error_string = "expirations are denied by broker"; break;
                case 148: error_string = "amount of open and pending orders has reached the limit"; break;
                case 149: error_string = "hedging is prohibited"; break;
                case 150: error_string = "prohibited by FIFO rules"; break;
                //---- mql4 errors
                case 4000: error_string = "no error (never generated code)"; break;
                case 4001: error_string = "wrong function pointer"; break;
                case 4002: error_string = "array index is out of range"; break;
                case 4003: error_string = "no memory for function call stack"; break;
                case 4004: error_string = "recursive stack overflow"; break;
                case 4005: error_string = "not enough stack for parameter"; break;
                case 4006: error_string = "no memory for parameter string"; break;
                case 4007: error_string = "no memory for temp string"; break;
                case 4008: error_string = "not initialized string"; break;
                case 4009: error_string = "not initialized string in array"; break;
                case 4010: error_string = "no memory for array\' string"; break;
                case 4011: error_string = "too long string"; break;
                case 4012: error_string = "remainder from zero divide"; break;
                case 4013: error_string = "zero divide"; break;
                case 4014: error_string = "unknown command"; break;
                case 4015: error_string = "wrong jump (never generated error)"; break;
                case 4016: error_string = "not initialized array"; break;
                case 4017: error_string = "dll calls are not allowed"; break;
                case 4018: error_string = "cannot load library"; break;
                case 4019: error_string = "cannot call function"; break;
                case 4020: error_string = "expert function calls are not allowed"; break;
                case 4021: error_string = "not enough memory for temp string returned from function"; break;
                case 4022: error_string = "system is busy (never generated error)"; break;
                case 4050: error_string = "invalid function parameters count"; break;
                case 4051: error_string = "invalid function parameter value"; break;
                case 4052: error_string = "string function internal error"; break;
                case 4053: error_string = "some array error"; break;
                case 4054: error_string = "incorrect series array using"; break;
                case 4055: error_string = "custom indicator error"; break;
                case 4056: error_string = "arrays are incompatible"; break;
                case 4057: error_string = "global variables processing error"; break;
                case 4058: error_string = "global variable not found"; break;
                case 4059: error_string = "function is not allowed in testing mode"; break;
                case 4060: error_string = "function is not confirmed"; break;
                case 4061: error_string = "send mail error"; break;
                case 4062: error_string = "string parameter expected"; break;
                case 4063: error_string = "integer parameter expected"; break;
                case 4064: error_string = "double parameter expected"; break;
                case 4065: error_string = "array as parameter expected"; break;
                case 4066: error_string = "requested history data in update state"; break;
                case 4099: error_string = "end of file"; break;
                case 4100: error_string = "some file error"; break;
                case 4101: error_string = "wrong file name"; break;
                case 4102: error_string = "too many opened files"; break;
                case 4103: error_string = "cannot open file"; break;
                case 4104: error_string = "incompatible access to a file"; break;
                case 4105: error_string = "no order selected"; break;
                case 4106: error_string = "unknown symbol"; break;
                case 4107: error_string = "invalid price parameter for trade function"; break;
                case 4108: error_string = "invalid ticket"; break;
                case 4109: error_string = "trade is not allowed in the expert properties"; break;
                case 4110: error_string = "longs are not allowed in the expert properties"; break;
                case 4111: error_string = "shorts are not allowed in the expert properties"; break;
                case 4200: error_string = "object is already exist"; break;
                case 4201: error_string = "unknown object property"; break;
                case 4202: error_string = "object is not exist"; break;
                case 4203: error_string = "unknown object type"; break;
                case 4204: error_string = "no object name"; break;
                case 4205: error_string = "object coordinates error"; break;
                case 4206: error_string = "no specified subwindow"; break;
                default: error_string = "unknown error"; break;
            }
            //----
            return (error_string);
        }
    }
}
