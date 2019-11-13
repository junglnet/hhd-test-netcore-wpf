using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hhb.Common.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Hhb.Repository.MongoDB.Test
{
    [TestClass]
    public class FundRepositoryTest
    {

        private Identificator f1FundTestId;

        private Fund f1Fund;

        public FundRepositoryTest()
        {
            
        }

       
        public async Task SimpleReadWriteAsyncTest()
        {

            f1FundTestId = Identificator.GenerateNewId();

            f1Fund = new Fund(f1FundTestId, "Test Fund 1", "Description", null);
                        

            var testResult1 = await TestFactoty.Current.FundRepository.AddAsync(f1Fund);

            Assert.AreEqual(f1FundTestId, testResult1);

            var testResult2 = await TestFactoty.Current.FundRepository.GetByIdAsync(f1FundTestId);

            Assert.AreEqual(f1Fund.Id.Id, testResult2.Id.Id);
        }
                

       // [TestMethod]
        public async Task NullReadWriteAsyncTest()
        {

           
            f1Fund = new Fund(null, "Test Fund 1", "Description", null);

                      
            Assert.IsNull(await TestFactoty.Current.FundRepository.AddAsync(f1Fund));

            Assert.IsNull(await TestFactoty.Current.FundRepository.GetByIdAsync(null));

            Assert.IsNull(await TestFactoty.Current.FundRepository.GetByIdAsync(Identificator.GenerateNewId()));
                        
        }

       // [TestMethod]
        public async Task DeleteTest()
        {

            f1FundTestId = Identificator.GenerateNewId();

            f1Fund = new Fund(f1FundTestId, "Test Fund 2", "Description", null);
                       
            await TestFactoty.Current.FundRepository.AddAsync(f1Fund);
                        
            Assert.IsTrue(await TestFactoty.Current.FundRepository.DeleteAsync(f1FundTestId));

        }

       // [TestMethod]
        public async Task NullDeleteTest()
        {
                        
            Assert.IsFalse(await TestFactoty.Current.FundRepository.DeleteAsync(Identificator.GenerateNewId()));

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                async() => await TestFactoty.Current.FundRepository.DeleteAsync(null));
                      
        }

        // [TestMethod]
         public async Task GetAllTest()
        {
                                             
            await TestFactoty.Current.FundRepository.AddAsync(new Fund(Identificator.GenerateNewId(), "Test Fund 1", "Description", null));

            await TestFactoty.Current.FundRepository.AddAsync(new Fund(Identificator.GenerateNewId(), "Test Fund 2", "Description", null));

            var result = await TestFactoty.Current.FundRepository.GetAllAsync();

            Assert.AreNotEqual(0, result.ToList().Count);

            Assert.IsNotNull(result);

        }

        // [TestMethod]
        public async Task GetByIdsTest()
        {
           
            List<Fund> funds = new List<Fund>()
            {
                new Fund(Identificator.GenerateNewId(), "Test Fund 1", "Description", null),
                new Fund(Identificator.GenerateNewId(), "Test Fund 1", "Description", null),
            };

            var ids = funds.Select(item => item.Id).ToArray();

            foreach(var fund in funds)
                await TestFactoty.Current.FundRepository.AddAsync(fund);

            
            var result = await TestFactoty.Current.FundRepository.GetByIdsAsync(ids);

            Assert.AreEqual(2, result.ToList().Count);            

        }

        [TestMethod]
        public async Task UpdateTest()
        {
                      

            var fund = new Fund(Identificator.GenerateNewId(), "Test Fund 1", "Description", null);
                      
            await TestFactoty.Current.FundRepository.AddAsync(fund);

            var fund1 = new Fund(fund.Id,"Test2", fund.Description, fund.Transactions);
                       
            var result = await TestFactoty.Current.FundRepository.UpdateAsync(fund1);

            Assert.IsTrue(result);

            var result2 = await TestFactoty.Current.FundRepository.GetByIdAsync(fund1.Id);

            Assert.AreEqual("Test2", result2.Name);

        }





    }
}
