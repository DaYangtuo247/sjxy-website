using System;
using System.Text.RegularExpressions;

namespace DevelopServer
{
    static class Common
    {
        public const string title = "这是一个很长很长很长很长很长很长的标题";
        public const string content = "截至2019年4月，学校有在职教职工1400余人，专任教师900余人，其中高级职称教师500余人，具有博士、硕士学位的教师超过专任教师总数的90%。教师队伍中，有国家“万人计划”领军人才、国家百千万人才工程人选、国家有突出贡献中青年专家、享受国务院政府特殊津贴人员、省高端人才引领培养计划第一层次人选、全国优秀教师、全国农业科研杰出人才、全国优秀工作者、教育部新世纪优秀人才等国家和省部级高端人才和专家名师70人次。学校聘有国家“高端外专”人选、湖北省“百人计划”和“楚天学者计划”等50余位知名学者，陈焕春院士、印遇龙院士和国家万人计划领军人才马勇、杨金海等专家名师担任学校特聘教授。";

        public static TValue GetPropertyValue<TValue>(object o, string name)
        {
            return (TValue)o.GetType().GetField(name).GetValue(o);
        }

        public static string EvaluateVariable(string source, string name, string value)
        {
            return source.Replace("${" + name + "}", value);
        }

        public static string EvaluateProperty(string objectName, string propertyName, object o, string source)
        {
            var v = o.GetType().GetField(propertyName).GetValue(o);
            if (v is string s)
            {
                return source.Replace("${" + objectName + "." + propertyName + "}", s);
            }
            else
            {
                throw new InvalidOperationException($"Error when evaluate property'{propertyName}' on object because it returns non string type.");
            }
        }

        public static string EvaluateAllProperty(string objectName, object o, string source)
        {
            return new Regex(@$"\${{{objectName}\.(.*?)}}").Replace(source, match =>
            {
                var propertyName = match.Groups[1].Value;
                var v = o.GetType().GetField(propertyName).GetValue(o);
                if (v is string s)
                    return s;
                else
                    throw new InvalidOperationException($"Error when evaluate property'{propertyName}' on object because it returns non string type.");
            });
        }

        private static readonly Regex vlinkRegex = new Regex(@"\${v_link\('(.*?)'\)}");

        public static string ConvertVLink(string source)
        {
            return vlinkRegex.Replace(source, (m) =>
            {
                return m.Groups[1].Value;
            });
        }
    }
}
