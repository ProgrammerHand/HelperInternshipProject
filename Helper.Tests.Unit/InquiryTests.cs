using Helper.Core;
using Helper.Core.Inquiry;
using Helper.Core.Inquiry.Exceptions;
using Helper.Core.Inquiry.ValueObjects;
using Moq;
using Shouldly;

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
            var inquiry = Inquiry.CreateInquiry("test", null, Variants.call) ;

            //ACT
            var exception = Record.Exception(inquiry.AcceptInquiry);

            //ASSERT
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<NoFeasibilityNoteWasGivenException>();

        }

        [Fact]
        public void given_inquiry_with_null_FeasibilityNote_rejectInquiry_should_fail()
        {
            //ARRANGE
            var inquiry = Inquiry.CreateInquiry("test", null, Variants.call);

            //ACT
            var exception = Record.Exception(inquiry.RejectInquiry);

            //ASSERT
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<NoFeasibilityNoteWasGivenException>();

        }

        [Theory]
        [InlineData("")]
        [InlineData("     ")]
        [InlineData(null)]
        public void given_empty_feasibilityNote_SetFeasibilityNote_rejectInquiry_should_fail(string feasibilityNote)
        {
            //ARRANGE
            var inquiry = Inquiry.CreateInquiry("test", null, Variants.call);

            //ACT
            var exception = Record.Exception(() => inquiry.SetFeasibilityNote(feasibilityNote));

            //ASSERT
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<NoFeasibilityNoteWasGivenException>();

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
        public void given_startDate_without_break_new_RealisationDate_should_fail()
        {
            //ARRANGE
            var startDate = mockClock.Object.Now.AddDays(3);

            //ACT
            var exception = Record.Exception(() => new RealisationDate(startDate, null, Variants.call, mockClock.Object));

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
        public void given_solutionVariant_Consulting_without_endDate_before_startDate_new_RealisationDate_should_fail()
        {
            //ARRANGE
            var solutionVariant = Variants.consulting;

            //ACT
            var exception = Record.Exception(() => new RealisationDate(mockClock.Object.Now.AddDays(9), null, solutionVariant, mockClock.Object));

            //ASSERT
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<NotGivenConsaltingEndException>();

        }
    }
}