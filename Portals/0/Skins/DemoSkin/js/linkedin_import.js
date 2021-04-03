//Script to mock the entry of LinkedIn data for the demo. 

$(document).ready(function () {

	"use strict";

	$('#linkedin_button').click(function () {

		$('#bio').val('Driven marketer, focused on strategic implementation, project management, creative direction, and branding. Thriving in creative and collaborative environments where a flurry of brainstorming turns into widely creative marketing campaigns. \r\n\r\nSpecialties: Marketing and Brand Strategy, Campaign Management, Website Strategy and Design, New Product Launches, ROI Analysis, Event Marketing, Traditional and Digital Advertising Strategy');
		$('#linkedin').val('https://www.linkedin.com/in/samrhodes');
		$('#twitter').val('@samrhodes');
		$('#facebook').val('');
		$('#education').val('University of Texas, Austin\r\nBS in Marketing');
		$('#work').val("SeniorCampaign Manager\r\nCompany Name\r\nRightNow Software\r\nDates Employed\r\nJan 2015 – Present\r\nEmployment Duration\r\n2 yr 10 mo\r\nLocation\r\nSunnyvale, CA\r\nResponsible for driving key campaigns from concept through release, working alongside Field and Global Marketing, Sales, Product Marketing and Operations. Coordination of targeting, planning, strategy, execution, campaign analysis, business insights, demand gen mix, cross-team collaboration, virtual team leadership and leadership reviews, and KPI and budget management.\r\n\r\nMarketing Technology Project Manager\r\nCompany Name\r\nSnapStream\r\nDates Employed\r\nMay 2013 – Dec 2014\r\nEmployment Duration\r\n1 yr 8 mo\r\nLocation\r\nAustin, Texas Area");
		$('#memorable').val('The birth of my twin sons.');
		$('#wish').val('Better insight on how all divisions within our department work. ');
		$('#collaborate').val('Love to collaborate with anyone and everyone who is responsible for customer success. Especially like working with the copywriters and the analytics groups.');
		$('#associations').val('AMA');
		$('#honors').val('PR News’ Social Media Icon Awards -Twitter | Best Marketing Campaign Honorable Mention');
		return false;

	});

});