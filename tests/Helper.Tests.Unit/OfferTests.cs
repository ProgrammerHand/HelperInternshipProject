using Helper.Core.Inquiry.ValueObjects;
using Helper.Core.Inquiry;
using Helper.Core.Utility;
using Moq;
using Helper.Core.Offer;
using Helper.Core.Offer.Exceptions;
using Helper.Core.User;
using Helper.Core.Exceptions;

namespace Helper.Tests.Unit
{
    public class OfferTests
    {
        #region ARRANGE
        private Mock<IClockCustom> mockClock;

        public OfferTests()
        {
            mockClock = new Mock<IClockCustom>();
            mockClock.Setup(fake => fake.Now)
                .Returns(new DateTime(2023, 8, 8, 12, 0, 0));
        }
        #endregion

        [Fact]
        public void given_PaymentDate_later_than_RealisationDate_setPaymentDate_should_fail()
        {

            //ARRANGE
            var user = User.CreateUser("test", "test");
            var realisationDate = new RealisationDate(mockClock.Object.Now.AddDays(10),
                mockClock.Object.Now.AddDays(20), Variants.call, mockClock.Object);
            var inquiry = Inquiry.CreateInquiry("test", realisationDate, Variants.call, user);
            inquiry.SetFeasibilityNote("test");
            var offer = Offer.CreateOffer(inquiry);

            //ACT
            var exception = Record.Exception(() => offer.AddPaymentDate(mockClock.Object.Now.AddDays(15)));

            //ASSERT
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<PaymentDateTooLateException>();
        }

        [Fact]
        public void given_OfferPrice_without_PaymentDate_SpecifyPrice_should_fail()
        {

            //ARRANGE
            var user = User.CreateUser("test", "test");
            var realisationDate = new RealisationDate(mockClock.Object.Now.AddDays(10),
                mockClock.Object.Now.AddDays(20), Variants.call, mockClock.Object);
            var inquiry = Inquiry.CreateInquiry("test", realisationDate, Variants.call, user);
            inquiry.SetFeasibilityNote("test");
            var offer = Offer.CreateOffer(inquiry);

            //ACT
            var exception = Record.Exception(() => offer.SpecifyPrice(20.00));

            //ASSERT
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<NoPaymentDateException>();
        }

        [Fact]
        public void given_wrong_OfferPrice_SpecifyPrice_should_fail()
        {

            //ARRANGE
            var user = User.CreateUser("test", "test");
            var realisationDate = new RealisationDate(mockClock.Object.Now.AddDays(10),
                mockClock.Object.Now.AddDays(20), Variants.call, mockClock.Object);
            var inquiry = Inquiry.CreateInquiry("test", realisationDate, Variants.call, user);
            inquiry.SetFeasibilityNote("test");
            var offer = Offer.CreateOffer(inquiry);
            offer.AddPaymentDate(mockClock.Object.Now.AddDays(5));

            //ACT
           var exception = Record.Exception(() => offer.SpecifyPrice(-5));

            //ASSERT
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InccorectPriceException>();
        }

        [Fact]
        public void given_draft_offer_AcceptOffer_should_fail()
        {

            //ARRANGE
            var user = User.CreateUser("test", "test");
            var realisationDate = new RealisationDate(mockClock.Object.Now.AddDays(10),
                mockClock.Object.Now.AddDays(20), Variants.call, mockClock.Object);
            var inquiry = Inquiry.CreateInquiry("test", realisationDate, Variants.call, user);
            inquiry.SetFeasibilityNote("test");
            var offer = Offer.CreateOffer(inquiry);
            offer.AddPaymentDate(mockClock.Object.Now.AddDays(5));
            offer.SpecifyPrice(10);

            //ACT
            var exception = Record.Exception(offer.Accept);

            //ASSERT
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<OfferNotReadyException>();
        }

        [Fact]
        public void given_accepted_offer_AcceptOffer_should_fail()
        {

            //ARRANGE
            var user = User.CreateUser("test", "test");
            var realisationDate = new RealisationDate(mockClock.Object.Now.AddDays(10),
                mockClock.Object.Now.AddDays(20), Variants.call, mockClock.Object);
            var inquiry = Inquiry.CreateInquiry("test", realisationDate, Variants.call, user);
            inquiry.SetFeasibilityNote("test");
            var offer = Offer.CreateOffer(inquiry);
            offer.AddPaymentDate(mockClock.Object.Now.AddDays(5));
            offer.SpecifyPrice(10);
            offer.FinalizeDraft();
            offer.Accept();

            //ACT
            var exception = Record.Exception(offer.Accept);

            //ASSERT
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<OfferDecisionAlredyGivenException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData("     ")]
        [InlineData(null)]
        public void given_finalized_offer_RejectOffer_withiut_reason_should_fail(string rejectReason)
        {

            //ARRANGE
            var user = User.CreateUser("test", "test");
            var realisationDate = new RealisationDate(mockClock.Object.Now.AddDays(10),
                mockClock.Object.Now.AddDays(20), Variants.call, mockClock.Object);
            var inquiry = Inquiry.CreateInquiry("test", realisationDate, Variants.call, user);
            inquiry.SetFeasibilityNote("test");
            var offer = Offer.CreateOffer(inquiry);
            offer.AddPaymentDate(mockClock.Object.Now.AddDays(5));
            offer.SpecifyPrice(10);
            offer.FinalizeDraft();

            //ACT
            var exception = Record.Exception(() => offer.Reject(rejectReason));

            //ASSERT
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<NoDescriptionGivenException>();
        }
    }
}
