using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ClearAction.Modules.ProfileClearAction_ProfileSetup.Linkedin
{

    [DataContract]
    public class LinkedinAccess
    {

        [DataMember(Name = "access_token")]
        public string access_token { get; set; }

        [DataMember(Name = "expires_in")]
        public int expires_in { get; set; }
    }

    [DataContract]
    public class EndDate
    {
        [DataMember(Name = "year")]
        public string year { get; set; }
    }

    [DataContract]
    public class StartDate
    {
        [DataMember(Name = "year")]
        public string year { get; set; }
    }

    [DataContract]
    public class Education
    {
        [DataMember(Name = "degree")]
        public string degree { get; set; }
        [DataMember(Name = "endDate")]
        public EndDate endDate { get; set; }
        [DataMember(Name = "fieldOfStudy")]
        public string fieldOfStudy { get; set; }
        [DataMember(Name = "id")]
        public int id { get; set; }
        [DataMember(Name = "schoolName")]
        public string schoolName { get; set; }
        [DataMember(Name = "startDate")]
        public StartDate startDate { get; set; }
        [DataMember(Name = "activities")]
        public string activities { get; set; }
    }

    [DataContract]
    public class Educations
    {
        [DataMember(Name = "_total")]
        public int _total { get; set; }
        [DataMember(Name = "values")]
        public IList<Education> values { get; set; }
    }

    [DataContract]
    public class phonevalues
    {
        [DataMember(Name = "phoneNumber")]
        public string phoneNumber { get; set; }
        [DataMember(Name = "phoneType")]
        public string phoneType { get; set; }
    }

    [DataContract]
    public class PhoneNumbers
    {
        [DataMember(Name = "_total")]
        public int _total { get; set; }
        [DataMember(Name = "values")]
        public IList<phonevalues> values { get; set; }
    }

    [DataContract]
    public class Company
    {
        [DataMember(Name = "name")]
        public string name { get; set; }
    }

    [DataContract]
    public class DateSet
    {
        [DataMember(Name = "month")]
        public string month { get; set; }
        [DataMember(Name = "year")]
        public string year { get; set; }
    }

    [DataContract]
    public class DateSet2
    {
        [DataMember(Name = "month")]
        public string month { get; set; }
        [DataMember(Name = "year")]
        public string year { get; set; }
    }


    [DataContract]
    public class Work
    {
        [DataMember(Name = "company")]
        public Company company { get; set; }
        [DataMember(Name = "id")]
        public int id { get; set; }
        [DataMember(Name = "isCurrent")]
        public bool isCurrent { get; set; }
        [DataMember(Name = "startDate")]
        public DateSet startDate { get; set; }
        [DataMember(Name = "summary")]
        public string summary { get; set; }
        [DataMember(Name = "title")]
        public string title { get; set; }
        [DataMember(Name = "endDate")]
        public DateSet2 endDate { get; set; }
    }

    [DataContract]
    public class Positions
    {
        [DataMember(Name = "_total")]
        public int _total { get; set; }
        [DataMember(Name = "values")]
        public IList<Work> values { get; set; }
    }

    [DataContract]
    public class Skill
    {
        [DataMember(Name = "name")]
        public string name { get; set; }
    }

    [DataContract]
    public class Skillset
    {
        [DataMember(Name = "id")]
        public int id { get; set; }
        [DataMember(Name = "skill")]
        public Skill skill { get; set; }
    }

    [DataContract]
    public class Skills
    {
        [DataMember(Name = "_total")]
        public int _total { get; set; }
        [DataMember(Name = "values")]
        public IList<Skillset> values { get; set; }
    }

    [DataContract]
    public class LinkEdin
    {
        [DataMember(Name = "educations")]
        public Educations educations { get; set; }
        [DataMember(Name = "emailAddress")]
        public string emailAddress { get; set; }
        [DataMember(Name = "firstName")]
        public string firstName { get; set; }
        [DataMember(Name = "headline")]
        public string headline { get; set; }
        [DataMember(Name = "id")]
        public string id { get; set; }
        [DataMember(Name = "industry")]
        public string industry { get; set; }
        [DataMember(Name = "lastName")]
        public string lastName { get; set; }
        [DataMember(Name = "phoneNumbers")]
        public PhoneNumbers phoneNumbers { get; set; }
        [DataMember(Name = "positions")]
        public Positions positions { get; set; }
        [DataMember(Name = "skills")]
        public Skills skills { get; set; }
        [DataMember(Name = "summary")]
        public string summary { get; set; }

        [DataMember(Name = "publicProfileUrl")]
        public string publicProfileUrl { get; set; }

        [DataMember(Name = "pictureUrl")]
        public string pictureUrl { get; set; }


        [DataMember(Name = "Location")]
        public location location { get; set; }
        [DataMember(Name = "CompanyName")]
        public string CompanyName
        {
            get
            {
                if (positions != null)
                    if (positions.values != null && positions.values.Count > 0)
                    {
                        return positions.values[0].company != null ? "" : positions.values[0].company.name;
                    }
                return "";

            }


            set {; }
        }




        [DataMember(Name = "Address")]
        public string Address
        {
            get
            {
                if (location != null)
                {
                    string strTemp = string.Empty;
                    strTemp = string.Format("{0}", location.name);

                    return strTemp;
                }
                return "";

            }


            set {; }
        }

        [DataMember(Name = "Education")]
        public string Education
        {
            get
            {
                if (educations != null)
                {
                    string strTemp = string.Empty;
                    foreach (Education o in educations.values)
                    {
                        strTemp = string.Format("{0}, {1} {2},{3}", o.degree, o.schoolName, o.fieldOfStudy, o.activities);
                    }

                    return strTemp;
                }
                return "";

            }


            set {; }
        }


        [DataMember(Name = "Postion")]
        public string Postion
        {
            get
            {
                if (positions != null)
                {
                    string strTemp = string.Empty;
                    foreach (Work o in positions.values)
                    {
                        strTemp = string.Format("{0}, {1} {2}, {3}", o.title, (o.company == null ? "" : o.company.name), (o.startDate == null ? "" : o.startDate.month + ", " + o.startDate.year), o.summary);
                    }

                    return strTemp;
                }
                return "";

            }


            set {; }
        }


    }
    [DataContract]
    public class location
    {
        [DataMember(Name = "country")]
        public country country { get; set; }

        [DataMember(Name = "name")]
        public string name { get; set; }
    }

    [DataContract]
    public class country
    {
        [DataMember(Name = "code")]
        public string code { get; set; }
    }


}