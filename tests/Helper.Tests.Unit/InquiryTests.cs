using Helper.Core.Inquiry;
using Helper.Core.Inquiry.Exceptions;
using Helper.Core.Inquiry.ValueObjects;
using Helper.Core.Utility;
using Moq;

namespace Helper.Tests.Unit
{

    public class InquiryTests
    {
        #region ARRANGE
        private Mock<IClockCustom> mockClock;

        public InquiryTests()
        {
            mockClock = new Mock<IClockCustom>();
            mockClock.Setup(fake => fake.Now)
                .Returns(new DateTime(2023, 8, 8, 12, 0, 0));
        }
        #endregion

        [Fact]
        public void given_inquiry_with_null_FeasibilityNote_acceptInquiry_should_fail()
        {
            //ARRANGE
            var inquiry = Inquiry.CreateInquiry("test", null, Variants.call, null) ;

            //ACT
            var exception = Record.Exception(inquiry.AcceptInquiry);

            //ASSERT
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<NoFeasibilityNoteException>();

        }

        [Fact]
        public void given_inquiry_with_null_FeasibilityNote_rejectInquiry_should_fail()
        {
            //ARRANGE
            var inquiry = Inquiry.CreateInquiry("test", null, Variants.call, null);

            //ACT
            var exception = Record.Exception(inquiry.RejectInquiry);

            //ASSERT
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<NoFeasibilityNoteException>();

        }

        [Theory]
        [InlineData("")]
        [InlineData("     ")]
        [InlineData(null)]
        public void given_empty_feasibilityNote_SetFeasibilityNote_should_fail(string feasibilityNote)
        {
            //ARRANGE
            var inquiry = Inquiry.CreateInquiry("test", null, Variants.call, null);

            //ACT
            var exception = Record.Exception(() => inquiry.SetFeasibilityNote(feasibilityNote));

            //ASSERT
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<NoFeasibilityNoteException>();

        }

        [Fact]
        public void given_startDate_before_today_new_RealisationDate_should_fail()
        {
            //ARRANGE
            var startDate = mockClock.Object.Now.AddDays(-1);

            //ACT
            var exception = Record.Exception(() => new RealisationDate(startDate, null, Variants.call, mockClock.Object));

            //ASSERT
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<StartDateTooEarly>();

        }

        [Fact]
        public void given_startDate_for_consultation_without_7days_break_new_RealisationDate_should_fail()
        {
            //ARRANGE
            var startDate = mockClock.Object.Now.AddDays(3);
            var endDate = mockClock.Object.Now.AddDays(8);

            //ACT
            var exception = Record.Exception(() => new RealisationDate(startDate, endDate, Variants.consulting, mockClock.Object));

            //ASSERT
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<StartDateTooEarly>();

        }

        [Fact]
        public void given_endDate_before_startDate_new_RealisationDate_should_fail()
        {
            //ARRANGE
            var startDate = mockClock.Object.Now.AddDays(9);
            var endDate = mockClock.Object.Now.AddDays(8);

            //ACT
            var exception = Record.Exception(() => new RealisationDate(startDate, endDate, Variants.call, mockClock.Object));

            //ASSERT
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<EndBeforeStartException>();

        }

        [Fact]
        public void given_solutionVariant_Consulting_without_endDate_new_RealisationDate_should_fail()
        {
            //ARRANGE
            var solutionVariant = Variants.consulting;

            //ACT
            var exception = Record.Exception(() => new RealisationDate(mockClock.Object.Now.AddDays(9), null, solutionVariant, mockClock.Object));

            //ASSERT
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<NotGivenConsaltingEndException>();

        }


        //[Fact]
        //public void given_feasibilityNote_when_updated_async_SetFeasiblityNoteDatabase_shoud_fail()
        //{
        //    //ARRANGE
        //    var inquiry = Inquiry.CreateInquiry("test", null, Variants.call, null);
        //    var mockSet = new Mock<DbSet<Inquiry>>();
        //    var mockContext = new Mock<HelperDbContext>();
        //    mockContext.Setup(x => x.Inquiries).Returns(mockSet.Object);

        //    var service = new InquiryRepository(mockContext.Object);
        //    service.AddAsync(inquiry);
        //    var inquiryChanged = inquiry;
        //    inquiryChanged.SetFeasibilityNote("test");
        //    var exception = Record.ExceptionAsync(async () => await service.UpdateAsync(inquiryChanged));


        //    //ACT

        //    //ASSERT
        //    exception.ShouldNotBeNull();
        //    exception.ShouldBeOfType<NotGivenConsaltingEndException>();

        //}
    }
}