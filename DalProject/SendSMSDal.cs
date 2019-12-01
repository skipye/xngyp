using DataBase;
using ModelProject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace DalProject
{
    public class SendSMSDal
    {
        public static bool Send(OrderModel OModels, int SendFlag)
        {
            string PostUrl = "http://106.ihuyi.cn/webservice/sms.php?method=Submit";
            string account = "C16242411";//查看用户名 登录用户中心->验证码通知短信>产品总览->API接口信息->APIID
            string password = "8eea6a039b627f4a57400570673053bb"; //查看密码 登录用户中心->验证码通知短信>产品总览->API接口信息->APIKEY
            string mobile = OModels.SendTel;
            Random rad = new Random();

            string MSG = "您好！订单" + OModels.OrderSN + "已审核，请尽快安排生产任务。";
            if (SendFlag == 1)
            {
                MSG = "您有新的订单！订单编号：" + OModels.OrderSN + "，下单人：" + OModels.SaleName + "。";
            }
            if (SendFlag == 3)
            {
                MSG = "您好！订单" + OModels.OrderSN + "已审核，请尽快安排生产任务。";
            }
            if (SendFlag == 4)
            {
                MSG = "您有新订单，请及时安排出库，收货人：" + OModels.Custmor + "订单号：" + OModels.OrderSN + "。";
            }

            string postStrTpl = "account={0}&password={1}&mobile={2}&content={3}";
            string SendContent = MSG;
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] postData = encoding.GetBytes(string.Format(postStrTpl, account, password, mobile, SendContent));

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(PostUrl);
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = postData.Length;

            Stream newStream = myRequest.GetRequestStream();
            // Send the data.
            newStream.Write(postData, 0, postData.Length);
            newStream.Flush();
            newStream.Close();

            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            if (myResponse.StatusCode == HttpStatusCode.OK)
            {
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);

                //Response.Write(reader.ReadToEnd());

                string res = reader.ReadToEnd();
                int len1 = res.IndexOf("</code>");
                int len2 = res.IndexOf("<code>");
                string code = res.Substring((len2 + 6), (len1 - len2 - 6));
                //Response.Write(code);
                int len3 = res.IndexOf("</msg>");
                int len4 = res.IndexOf("<msg>");
                string msg = res.Substring((len4 + 5), (len3 - len4 - 5));
                //Response.Write(msg);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
