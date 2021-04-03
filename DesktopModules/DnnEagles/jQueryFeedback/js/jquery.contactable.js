/*
* contactable 1.2.1 - jQuery Ajax contact form
*
* Copyright (c) 2009 Philip Beel (http://www.theodin.co.uk/)
* Dual licensed under the MIT (http://www.opensource.org/licenses/mit-license.php) 
* and GPL (http://www.opensource.org/licenses/gpl-license.php) licenses.
*
* Revision: jQueryId: jquery.contactable.js 2010-01-18 jQuery
*
*/

//extend the plugin
(function (jQuery) {
    //define the new for the plugin ans how to call it	
    jQuery.fn.contactable = function (options) {
        //set default options  
        var defaults = {
            url: '/DesktopModules/DnnEagles/jQueryFeedback/Mailer.ashx?portalID=' + portalID,
            name: 'Name',
            email: 'Email',
            message: 'Message',
            subject: 'A contactable message',
            submit: 'SEND',
            recievedMsg: 'Thank you for your message',
            notRecievedMsg: 'Sorry but your message could not be sent, try again later',
            disclaimer: 'Please feel free to get in touch, we value your feedback',
            hideOnSubmit: false

        };

        //call in the default otions
        var options = jQuery.extend(defaults, options);
        //act upon the element that is passed into the design    
        return this.each(function () {
            //construct the form
            var this_id_prefix = '#' + this.id + ' ';
            jQuery('#Body').append('<div id="contactable"><div id="contactable_inner"><p>Contact&nbsp;Us</p></div><form id="contactForm" method="" action=""><div id="loading"></div><div id="callback"></div><div class="holder"><p><label for="name">' + options.name + '<span class="red"> * </span></label><br /><input id="name" class="contact" name="name"  /></p><p><label for="email">' + options.email + ' <span class="red"> * </span></label><br /><input id="email" class="contact" name="email" /></p><p><label for="message">' + options.message + ' <span class="red"> * </span></label><br /><textarea id="message" name="message" class="message" rows="4" cols="30" ></textarea></p><p><input class="submit" type="submit" value="' + options.submit + '"/></p><p class="disclaimer">' + options.disclaimer + '</p></div></form></div>');
            //jQuery(this).html('<div id="contactable_inner"></div><div id="contactForm"><div id="loading"></div><div id="callback"></div><div class="holder"><p><label for="name">' + options.name + '<span class="red"> * </span></label><br /><input id="name" class="contact" name="name"/></p><p><label for="email">' + options.email + ' <span class="red"> * </span></label><br /><input id="email" class="contact" name="email" /></p><p><label for="message">' + options.message + ' <span class="red"> * </span></label><br /><textarea id="message" name="message" class="message" rows="4" cols="30" ></textarea></p><p><input class="submit" type="submit" onClick="" value="' + options.submit + '"/></p><p class="disclaimer">' + options.disclaimer + '</p></div></div>');
            //show / hide function
            jQuery(this_id_prefix + 'div#contactable_inner').toggle(function () {

                jQuery(this_id_prefix + '#name').val(fbName);
                jQuery(this_id_prefix + '#email').val(fbEmail);

                if (fbName.length > 0) {
                    jQuery(this_id_prefix + '#name').prop('disabled', true);
                    jQuery(this_id_prefix + '#email').prop('disabled', true);
                }

                jQuery(this_id_prefix + '#overlay').css({ display: 'block' });
                jQuery(this).animate({ "marginBottom": "-=5px" }, "fast");
                jQuery(this_id_prefix + '#contactForm').animate({ "marginBottom": "-=0px" }, "fast");
                jQuery(this).animate({ "marginBottom": "+=380px" }, "slow"); //360
                jQuery(this_id_prefix + '#contactForm').animate({ "marginBottom": "+=390px" }, "slow");
            },
			function () {
			    jQuery(this_id_prefix + '#contactForm').animate({ "marginBottom": "-=390px" }, "slow");
			    jQuery(this).animate({ "marginBottom": "-=380px" }, "slow").animate({ "marginBottom": "+=5px" }, "fast");
			    jQuery(this_id_prefix + '#overlay').css({ display: 'none' });
			});

            //validate the form 
            jQuery(this_id_prefix + "#contactForm").validate({
                //set the rules for the fild names
                rules: {
                    name: {
                        required: true,
                        minlength: 2
                    },
                    email: {
                        required: true,
                        email: true
                    },
                    message: {
                        required: true
                    }
                },
                //set messages to appear inline
                messages: {
                    name: "",
                    email: "",
                    message: ""
                },

                submitHandler: function () {
                    jQuery(this_id_prefix + '.holder').hide();
                    jQuery(this_id_prefix + '#loading').show();
                    jQuery.ajax({
                        type: 'POST',
                        url: options.url,
                        data: { subject: options.subject, name: jQuery(this_id_prefix + '#name').val(), email: jQuery(this_id_prefix + '#email').val(), message: jQuery(this_id_prefix + '#message').val(), locURL: location.href },
                        success: function (data) {
                            jQuery(this_id_prefix + '#loading').css({ display: 'none' });
                            if (data == 'success') {
                                jQuery(this_id_prefix + '#callback').show().append(options.recievedMsg);
							   if (options.hideOnSubmit == true) {
                                    //hide the tab after successful submition if requested
							       jQuery(this_id_prefix + '#contactForm').animate({ dummy: 1 }, 2000).animate({ "marginBottom": "-=450px" }, "slow");
							       jQuery(this_id_prefix + 'div#contactable_inner').animate({ dummy: 1 }, 2000).animate({ "marginBottom": "-=447px" }, "slow").animate({ "marginBottom": "+=5px" }, "fast");
                                    jQuery(this_id_prefix + '#overlay').css({ display: 'none' });
									}
									else
									{
									  //Added by Kusum - else case to hide the thank you message and whole form. reset form with blank values.	
										setTimeout(function () {
										jQuery(this_id_prefix + 'div#contactable_inner').click();
									    jQuery(this_id_prefix + '#callback').hide();
										jQuery(this_id_prefix + '#callback').html('');
									    jQuery(this_id_prefix + '#name').val('');
										jQuery(this_id_prefix + '#email').val('');
										jQuery(this_id_prefix + '#message').val('');
										jQuery(this_id_prefix + '.holder').show();
										
										jQuery(this_id_prefix + '#name').css("background-color","#ffffff");
										jQuery(this_id_prefix + '#email').css("background-color","#ffffff");
										jQuery(this_id_prefix + '#message').css("background-color","#ffffff");
										
                                    	}, 2000);
												
									}
                            } else {
								
								jQuery(this_id_prefix + '#callback').show().append(options.notRecievedMsg);
								setTimeout(function () {
                                    jQuery(this_id_prefix + '.holder').show();
                                    jQuery(this_id_prefix + '#callback').hide().html('');
                                }, 2000);
                            }
                        },
                        error: function () {
                            jQuery(this_id_prefix + '#loading').css({ display: 'none' });
                            jQuery(this_id_prefix + '#callback').show().append(options.notRecievedMsg);
                        }
                    });
                }
            });
        });
    };

})(jQuery);

