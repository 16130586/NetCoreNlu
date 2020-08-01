using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetProject.Util
{
    public class CheckValidate
    {
        public static bool checkIsEmpty(String input)
        {
            return string.IsNullOrEmpty(input);
        }

        public static bool checkIsEmail(String email)
        {
            //String emailRegex = "^[a-zA-Z0-9_+&*-]+(?:\\." +
            //        "[a-zA-Z0-9_+&*-]+)*@" +
            //        "(?:[a-zA-Z0-9-]+\\.)+[a-z" +
            //        "A-Z]{2,7}$";

            //Pattern pat = Pattern.compile(emailRegex);
            //if (email == null)
            //    return false;
            //return pat.matcher(email).matches();
            return true;
        }

        public static bool checkIsPass(String pass)
        {
            //String emailRegex = "(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,}";

            //Pattern pat = Pattern.compile(emailRegex);
            //if (pass == null)
            //    return false;
            //return pat.matcher(pass).matches();
            return true;
        }

        public static bool checkIsNumPhone(String numPhone)
        {
            //String emailRegex = "^[0-9-+\\s()]*$";

            //Pattern pat = Pattern.compile(emailRegex);
            //if (numPhone == null)
            //    return false;
            //return pat.matcher(numPhone).matches();
            return true;
        }
    }
}
