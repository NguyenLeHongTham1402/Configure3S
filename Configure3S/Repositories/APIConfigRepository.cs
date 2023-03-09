using Configure3S.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Configure3S.Repositories
{
    public class APIConfigRepository
    {
        private Connfigure3SContext context;

        //Hàm cho User

        public List<TbApiconfigure> GetListAPIConfigByCompanyId(string companyID)
        {
            List<TbApiconfigure> results = new List<TbApiconfigure> { };
            using(context = new Connfigure3SContext())
            {
                try
                {
                    results = context.TbApiconfigures.Where(x => x.CompanyId == companyID).ToList();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return results;
        }

        public string CreateAPIConfig(IFormCollection collection)
        {
            string result = string.Empty;

            TbApiconfigure config = new TbApiconfigure();
            config.CompanyId = collection["CompanyId"].ToString();
            config.Apikey = collection["Apikey"].ToString();
            config.Urlapi = collection["Urlapi"].ToString();
            config.UserName = collection["UserName"].ToString();
            config.Password = collection["Password"].ToString();

            using(context = new Connfigure3SContext())
            {
                using(var trans = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.TbApiconfigures.Add(config);
                        int i = context.SaveChanges();
                        if (i > 0)
                        {
                            trans.Commit();
                            result = "t";
                        }                        
                        else
                        {
                            trans.Rollback();
                            result = "f";                           
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        result = ex.Message;
                    }
                }
            }
            return result;
        }

        public string UpdateAPIConfig(IFormCollection collection, int sourceID)
        {
            string result = string.Empty;

            TbApiconfigure config = new TbApiconfigure();
            config.SourceId = sourceID;
            config.CompanyId = collection["CompanyId"].ToString();
            config.Apikey = collection["Apikey"].ToString();
            config.Urlapi = collection["Urlapi"].ToString();
            config.UserName = collection["UserName"].ToString();
            config.Password = collection["Password"].ToString();

            using(context = new Connfigure3SContext())
            {
                using(var trans = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.TbApiconfigures.Update(config);
                        int i = context.SaveChanges();
                        if (i > 0)
                        {
                            trans.Commit();
                            result = "t";
                        }
                        else
                        {
                            trans.Rollback();
                            result = "f";
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        result = ex.Message;
                    }
                }
            }
            return result;
        }

        public string DeleteAPIConfig(int sourceID)
        {
            string result = string.Empty;

            using(context = new Connfigure3SContext())
            {
                using(var trans = context.Database.BeginTransaction())
                {
                    try
                    {
                        var p = context.TbApiconfigures.SingleOrDefault(x => x.SourceId == sourceID);
                        if (p != null)
                        {
                            context.TbApiconfigures.Remove(p);
                            int i = context.SaveChanges();
                            if (i > 0)
                            {
                                trans.Commit();
                                result = "t";
                            }
                            else
                            {
                                trans.Rollback();
                                result = "f";
                            }
                        }
                        else
                        {
                            result = "f";
                        }
                    }
                    catch(Exception ex)
                    {
                        trans.Rollback();
                        result = ex.Message;
                    }
                }
            }
            return result;
        }

        public TbApiconfigure GetApiconfigureById(int source)
        {
            TbApiconfigure conf = new TbApiconfigure();
            try
            {
                conf = context.TbApiconfigures.SingleOrDefault(x => x.SourceId == source);
            }
            catch (Exception)
            {
                throw;
            }
            return conf;
        }

        //Hàm cho Admin

        public List<TbApiconfigure> GetAllApiconfigures()
        {
            List<TbApiconfigure> apiconfigures = new List<TbApiconfigure> { };
            using (context = new Connfigure3SContext())
            {
                try
                {
                    apiconfigures = context.TbApiconfigures.Select(s => s).ToList();
                }
                catch (Exception)
                {
                    throw;
                } 
            }
            return apiconfigures;
        }
    }
}
