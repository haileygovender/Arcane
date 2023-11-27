using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ST10084623_PROG7312
{
     class LoadList
    {

        public ArrayList loadListBox()
        {


            ArrayList list = new ArrayList();
            Random random = new Random();

            for (int i = 0; i < 10; i++)
            {
                StringBuilder randomValue = new StringBuilder();

                // Generate a random number with two decimal places
                double randomNumber = random.NextDouble() * 1000;
                string formattedNumber = randomNumber.ToString("0.00");

                // Append the formatted number to the StringBuilder
                randomValue.Append(formattedNumber + "\t");

                // Generate and append three random letters
                for (int j = 0; j < 3; j++)
                {
                    char randomLetter = (char)('A' + random.Next(26));
                    randomValue.Append(randomLetter);
                }
                list.Add(randomValue);
            }
            return list;
        }



    }
}
