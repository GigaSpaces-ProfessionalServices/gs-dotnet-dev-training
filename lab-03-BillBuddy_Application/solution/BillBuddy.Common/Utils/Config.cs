using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BillBuddy.Common.Utils
{
    public class Config
    {
        public static List<string> MerchantList
        {
            get
            {
                return new List<string>(
                        new string[]{
                            "Like Pace",
				            "Konegsad,",
				            "Hautika",
				            "SomeDisk",
				            "Swakowsky",
				            "Green Head band",
				            "Shiruckou",
				            "Mazalaty",
				            "Eagle",
				            "Lohitech",
				            "jewlary 4 U",
				            "The musicals",
				            "SoccerMaster",
				            "Fort",
				            "2-Times",
				            "Gems"
                    });
            }
        }

        public static List<string> UserList
        {
            get
            {
                return new List<string>(
                            new string[] {"James Johnson",
				            "Peter Gardener",
				            "Andrei Saizovsky",
				            "Petr Kirul",
				            "Gerard Bourtagne",
				            "Hans Freihof",
				            "Sami Filppula",
				            "Niklas Nilsson",
				            "Marian Varga",
				            "Sigur Briem",
				            "Bill Klien",
				            "David King",
				            "Magic Jordan",
				            "Hana Brill",
				            "Mustafa Cohen",
				            "Michel Peet",
				            "Samnta Gold",
				            "Snoop Cat",
				            "Marian Vog",
				            "Suger Baby"});
            }
        }
    }
}
