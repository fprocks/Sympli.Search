using FluentAssertions;
using Sympli.Search.Usecases.HtmlParser;
using System.Collections.Generic;
using Xunit;

namespace Sympli.Search.UnitTests
{
    public class HtmlParsing
    {
        [Theory]
        [InlineData("www.lawsociety.com.au", "2,4")]
        [InlineData("dsslaw.com.au", "0")]
        public void Should_return_positions_for_google_search(string url, string expected)
        {
            var parser = new HtmlParser();

            var htmls = new List<string>() { GoogleSearchResult };
            var actual = parser.Parse(htmls, url, "<a href=\"http(s)?[a-zA-Z0-9--?=/]*\" data-ved=");

            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("www.sympli.com.au", "1")]
        [InlineData("www.legalpracticeintelligence.com.au", "0")]
        public void Should_return_positions_for_bing_search(string url, string expected)
        {
            var parser = new HtmlParser();

            var htmls = new List<string>() { BingSearchResult };
            var actual = parser.Parse(htmls, url, "<a href=\"http(s)?[a-zA-Z0-9--?=/]*\" h=");

            actual.Should().BeEquivalentTo(expected);
        }

        private string GoogleSearchResult =
        @"<div class=""g"">
			  <div class=""tF2Cxc"" data-hveid=""CCQQAA"" data-ved=""2ahUKEwi5oK38sdnuAhVQzTgGHSZpAOwQFSgAMA56BAgkEAA"">
				 <div class=""yuRUbf""><a href=""https://www.pexa.com.au/"" data-ved=""2ahUKEwi5oK38sdnuAhVQzTgGHSZpAOwQFjAOegQIJBAC"" ping=""/url?sa=t&amp;source=web&amp;rct=j&amp;url=https://www.pexa.com.au/&amp;ved=2ahUKEwi5oK38sdnuAhVQzTgGHSZpAOwQFjAOegQIJBAC"">
					   <br>
					   <h3 class=""LC20lb DKV0Md""><span>PEXA: Home</span></h3>
					   <div class=""TbwUpd NJjxre""><cite class=""iUh30 Zu0yb tjvcx"">www.pexa.com.au</cite></div>
					</a>
				 </div>
			  </div>
		   </div>
		   <div class=""g"">
			  <div class=""tF2Cxc"" data-hveid=""CCAQAA"" data-ved=""2ahUKEwi5oK38sdnuAhVQzTgGHSZpAOwQFSgAMA96BAggEAA"">
				 <div class=""yuRUbf""><a href=""https://www.lawsociety.com.au/resources/resources/my-practice-area/electronic-conveyancing/econveyancing-verdict"" data-ved=""2ahUKEwi5oK38sdnuAhVQzTgGHSZpAOwQFjAPegQIIBAC"" ping=""/url?sa=t&amp;source=web&amp;rct=j&amp;url=https://www.lawsociety.com.au/resources/resources/my-practice-area/electronic-conveyancing/econveyancing-verdict&amp;ved=2ahUKEwi5oK38sdnuAhVQzTgGHSZpAOwQFjAPegQIIBAC"">
					   <br>
					   <h3 class=""LC20lb DKV0Md""><span>E-Conveyancing - The Verdict | The Law Society of NSW</span></h3>
					   <div class=""TbwUpd NJjxre""><cite class=""iUh30 Zu0yb qLRx3b tjvcx"">www.lawsociety.com.au<span class=""dyjrff qzEoUe""><span> › electronic-conveyancing › ec...</span></span></cite></div>
					</a>
				 </div>
			  </div>
		   </div>
		   <div class=""g"">
			  <div class=""tF2Cxc"" data-hveid=""CB4QAA"" data-ved=""2ahUKEwi5oK38sdnuAhVQzTgGHSZpAOwQFSgAMBB6BAgeEAA"">
				 <div class=""yuRUbf""><a href=""https://www.lawyersconveyancing.com.au/what-is-electronic-conveyancing/"" data-ved=""2ahUKEwi5oK38sdnuAhVQzTgGHSZpAOwQFjAQegQIHhAC"" ping=""/url?sa=t&amp;source=web&amp;rct=j&amp;url=https://www.lawyersconveyancing.com.au/what-is-electronic-conveyancing/&amp;ved=2ahUKEwi5oK38sdnuAhVQzTgGHSZpAOwQFjAQegQIHhAC"">
					   <br>
					   <h3 class=""LC20lb DKV0Md""><span>What is Electronic Conveyancing? - Lawyers Conveyancing</span></h3>
					   <div class=""TbwUpd NJjxre""><cite class=""iUh30 Zu0yb qLRx3b tjvcx"">www.lawyersconveyancing.com.au<span class=""dyjrff qzEoUe""><span> › what-is-electronic...</span></span></cite></div>
					</a>
				 </div>
			  </div>
		   </div>   
		   <div class=""g"">
			  <div class=""tF2Cxc"" data-hveid=""CCAQAA"" data-ved=""2ahUKEwi5oK38sdnuAhVQzTgGHSZpAOwQFSgAMA96BAggEAA"">
				 <div class=""yuRUbf""><a href=""https://www.lawsociety.com.au/resources/resources/my-practice-area/electronic-conveyancing/econveyancing-verdict"" data-ved=""2ahUKEwi5oK38sdnuAhVQzTgGHSZpAOwQFjAPegQIIBAC"" ping=""/url?sa=t&amp;source=web&amp;rct=j&amp;url=https://www.lawsociety.com.au/resources/resources/my-practice-area/electronic-conveyancing/econveyancing-verdict&amp;ved=2ahUKEwi5oK38sdnuAhVQzTgGHSZpAOwQFjAPegQIIBAC"">
					   <br>
					   <h3 class=""LC20lb DKV0Md""><span>E-Conveyancing - The Verdict | The Law Society of NSW</span></h3>
					   <div class=""TbwUpd NJjxre""><cite class=""iUh30 Zu0yb qLRx3b tjvcx"">www.lawsociety.com.au<span class=""dyjrff qzEoUe""><span> › electronic-conveyancing › ec...</span></span></cite></div>
					</a>
				 </div>
			  </div>
		   </div>";

        private string BingSearchResult =
        @"<li class=""b_algo""><h2><a href=""https://www.sympli.com.au/half-day-seminars-on-latest-technologies-for-e-conveyancing-complimentary-cpd/"" h=""ID=SERP,5724.1"">Risks &amp; Rewards of e-conveyancing – Legal Practice ...</a></h2>
            <div class=""b_caption"">
                <div class=""b_attribution"" u=""36|5102|4755585524240431|xydrXcsbiUVyl1Msw2KR7zBESwcKAm0F""><cite>https://www.legalpracticeintelligence.com.au/half-day-seminars-on...</cite><span class=""c_tlbxTrg""><span class=""c_tlbxH"" H=""BASE:CACHEDPAGEDEFAULT"" K=""SERP,5725.1""></span></span></div>
                <p>The Risks <strong>&amp;</strong> Rewards of e-conveyancing seminars, to be held in Sydney, Melbourne and Brisbane, will explore the current risks and rewards specific to e-conveyancing. Attendees will gain a full rounded perspective from practitioners, state government and legal-tech innovators with a mix of presentations and panel discussions on topical subjects addressing: The true impact of the property market ...</p>
            </div>
        </li>
        <li class=""b_algo""><h2><a href=""https://lodgex.com.au/process/"" h=""ID=SERP,5736.1"">Process - LodgeX</a></h2>
            <div class=""b_caption"">
                <div class=""b_attribution""><cite>https://lodgex.com.au/process</cite></div>
                <p>b'LodgeXThe future of e-settlements hasarrived Regulated, insured, trusted.LodgeX is powered by LodgeX Legal'</p>
            </div>
        </li>
        <li class=""b_algo""><h2><a href=""https://info.infotrack.com.au/the-practitioner-playbook-edition-13"" h=""ID=SERP,5749.1"">E-conveyancing update &amp; sales tips</a></h2>
            <div class=""b_caption"">
                <div class=""b_attribution"" u=""38|5104|4732212308872718|ebdIgAB-zktn9kLKn5C1QqbR5Zh5g5ja""><cite>https://info.infotrack.com.au/the-practitioner-playbook-edition-13</cite><span class=""c_tlbxTrg""><span class=""c_tlbxH"" H=""BASE:CACHEDPAGEDEFAULT"" K=""SERP,5750.1""></span></span></div>
                <p>A young lawyer’s commentary on the shift towards e-conveyancing. Having had a handful of confusing experiences with the manual conveyancing process, the opportunity to settle property matters through a digital platform is comforting. Read more. The NSW mandate is on its way, so what are the risks and rewards of e-conveyancing? As we move into the future, getting on the front foot with legal ...</p>
            </div>
        </li>";

    }
}
