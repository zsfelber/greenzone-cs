//+------------------------------------------------------------------+
//| Copyright 2009 OpenThinking Systems, LLC
//|
//|  Licensed under the Apache License, Version 2.0 (the "License");
//|  you may not use this file except in compliance with the License.
//|  You may obtain a copy of the License at
//|
//|               http://www.apache.org/licenses/LICENSE-2.0
//|
//|  Unless required by applicable law or agreed to in writing,
//|  software distributed under the License is distributed on an
//|  "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
//|  KIND, either express or implied. See the License for the
//|  specific language governing permissions and limitations under
//|  the License.
//+------------------------------------------------------------------+

#include <iostream>
#include <sstream>
#include <string>
#include <stdexcept>

class BadConversion : public std::runtime_error
{
public:
    BadConversion(const std::string& s)
            : std::runtime_error(s)
    { }
};

inline std::string stringify(double x)
{
    std::ostringstream o;
    if (!(o << x))
        throw BadConversion("stringify(double)");
    return o.str();
}

inline std::string stringify(long x)
{
    std::ostringstream o;
    if (!(o << x))
        throw BadConversion("stringify(long)");
    return o.str();
}

inline std::string stringify(int x)
{
    std::ostringstream o;
    if (!(o << x))
        throw BadConversion("stringify(int)");
    return o.str();
}

inline double convertToDouble(const std::string& s)
{
    std::istringstream i(s);
    double x;
    if (!(i >> x))
        throw BadConversion("convertToDouble(\"" + s + "\")");
    return x;
}

inline double convertToDouble(const char* s)
{
    std::istringstream i(s);
    double x;
    if (!(i >> x))
        throw BadConversion("convertToDouble");
    return x;
}
inline int convertToInt(const std::string& s)
{
    std::istringstream i(s);
    int x;
    if (!(i >> x))
        throw BadConversion("convertToInt(\"" + s + "\")");
    return x;
}

inline int convertToInt(const char* s)
{
    std::istringstream i(s);
    int x;
    if (!(i >> x))
        throw BadConversion("convertToInt");
    return x;
}
