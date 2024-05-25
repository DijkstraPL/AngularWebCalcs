using Build_IT_DataAccess.SteelProfiles.Entities;
using Build_IT_DataAccess.SteelProfiles.Entities.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Build_IT_DataAccess_SqlServer.SteelProfiles
{
    public static class SteelProfilesDbSeeder
    {
        public static ProfileType IPE { get; private set; }

        public static void Seed()
        {
            var profileType = new ProfileType
            {
                Name = "IPE"
            };

            profileType.Parameters.Add(new Parameter
            {
                Name = "h",
                Unit = "mm",
                Description = "Height."
            });
            profileType.Parameters.Add(new Parameter
            {
                Name = "b",
                Unit = "mm",
                Description = "Width."
            });
            profileType.Parameters.Add(new Parameter
            {
                Name = "t_f_",
                Unit = "mm",
                Description = "Flange thickness."
            });
            profileType.Parameters.Add(new Parameter
            {
                Name = "t_w_",
                Unit = "mm",
                Description = "Web thickness."
            });
            profileType.Parameters.Add(new Parameter
            {
                Name = "r",
                Unit = "mm",
                Description = "Radius."
            });

            profileType.SectionPoints.Add(new SectionPoint
            {
                X = "0",
                Y = "0",
            });
            profileType.SectionPoints.Add(new SectionPoint
            {
                X = "[b]",
                Y = "0",
            });
            profileType.SectionPoints.Add(new SectionPoint
            {
                X = "[b]",
                Y = "[t_f_]",
            });
            profileType.SectionPoints.Add(new SectionPoint
            {
                X = "[b] / 2 + [t_w_] / 2",
                Y = "[t_f_]",
                ChamferType = ChamferType.Round,
                ChamferX = "[r]"
            });
            profileType.SectionPoints.Add(new SectionPoint
            {
                X = "[b] / 2 + [t_w_] / 2",
                Y = "[h] - [t_f_]",
                ChamferType = ChamferType.Round,
                ChamferX = "[r]"
            });
            profileType.SectionPoints.Add(new SectionPoint
            {
                X = "[b]",
                Y = "[h] - [t_f_]",
            });
            profileType.SectionPoints.Add(new SectionPoint
            {
                X = "[b]",
                Y = "[h]",
            });
            profileType.SectionPoints.Add(new SectionPoint
            {
                X = "0",
                Y = "[h]",
            });
            profileType.SectionPoints.Add(new SectionPoint
            {
                X = "0",
                Y = "[h] - [t_f_]",
            });
            profileType.SectionPoints.Add(new SectionPoint
            {
                X = "[b] / 2 - [t_w_] / 2",
                Y = "[h] - [t_f_]",
                ChamferType = ChamferType.Round,
                ChamferX = "[r]"
            });
            profileType.SectionPoints.Add(new SectionPoint
            {
                X = "[b] / 2 - [t_w_] / 2",
                Y = "[t_f_]",
                ChamferType = ChamferType.Round,
                ChamferX = "[r]"
            });
            profileType.SectionPoints.Add(new SectionPoint
            {
                X = "0",
                Y = "[t_f_]",
            });

            var datas = new List<string>
            {
                "IPE 80,80,46,3.8,5.2,5",
                "IPE 100,100,55,4.1,5.7,7",
                "IPE 120,120,64,4.4,6.3,7",
                "IPE 140,140,73,4.7,6.9,7",
                "IPE 160,160,82,5,7.4,9",
                "IPE 180,180,91,5.3,8,9",
                "IPE 200,200,100,5.6,8.5,12",
                "IPE 220,220,110,5.9,9.2,12",
                "IPE 240,240,120,6.2,9.8,15",
                "IPE 270,270,135,6.6,10.2,15",
                "IPE 300,300,150,7.1,10.7,15",
                "IPE 330,330,160,7.5,11.5,18",
                "IPE 360,360,170,8,12.7,18",
                "IPE 400,400,180,8.6,13.5,21",
                "IPE 450,450,190,9.4,14.6,21",
                "IPE 500,500,200,10.2,16,21",
                "IPE 550,550,210,11.1,17.2,24",
                "IPE 600,600,220,12,19,24",
            };

            foreach (var data in datas)
            {
                var values = data.Split(',');

                var profile = new SteelProfile
                {
                    Name = values[0]
                };

                int index = 0;
                foreach (var value in values.Skip(1))
                {
                    profile.ParametersValues.Add(new ParameterValue
                    {
                        Parameter = profileType.Parameters.ElementAt(index),
                        Value = value
                    });
                    index++;
                }               

                profileType.SteelProfiles.Add(profile);
            }

            IPE = profileType;
        }
    }
}
