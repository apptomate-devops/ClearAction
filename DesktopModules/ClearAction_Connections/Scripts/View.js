

function CADirectory($, ko, settings, composeMessageSettings) {

    var opts = $.extend({}, CADirectory.defaultSettings, settings);
    var serviceFramework = settings.servicesFramework;
    var pageIndex = 0;
    var pageSize = opts.pageSize;
    var userId = opts.userId;
    var groupId = opts.groupId;
    var viewerId = opts.viewerId;
    var profileUrl = opts.profileUrl;
    var profileUrlUserToken = opts.profileUrlUserToken;
    var profilePicHandler = opts.profilePicHandler;
    var containerElement = null;
    var OrderBy = opts.OrderBy;
    var tCount = 0;
    var iViewType = opts.ViewType;
    var iFilterBy = opts.FilterBy;

   
    function displayMessage(message, cssclass) {
        var messageNode = $("<div/>").addClass('dnnFormMessage ' + cssclass).text(message);
        $(containerElement).prepend(messageNode);
        messageNode.fadeOut(3000, 'easeInExpo', function () { messageNode.remove(); });
    };

    function Member(item) {

        var self = this;

        self.AddFriendText = opts.addFriendText;
        self.AcceptFriendText = opts.acceptFriendText;
        self.FriendPendingText = opts.friendPendingText;
        self.RemoveFriendText = opts.removeFriendText;
        self.FollowText = opts.followText;
        self.UnFollowText = opts.unFollowText;
        self.SendMessageText = opts.sendMessageText;
        self.UserNameText = opts.userNameText;
        self.EmailText = opts.emailText;
        self.CityText = opts.cityText;

        self.RoleName = ko.observable(item.RoleName);
        self.CompanyName = ko.observable(item.CompanyName);
        self.JobTitle = ko.observable(item.JobTitle);
        self.ToolTipId = '#tooltip' + ko.observable(item.UserId);
        self.UserId = ko.observable(item.MemberId);
        self.LastName = ko.observable(item.LastName);
        self.FirstName = ko.observable(item.FirstName);
        self.UserName = ko.observable(item.UserName);
        self.DisplayName = ko.observable(item.DisplayName);
        self.Email = ko.observable(item.Email);
        self.sortMode = ko.observable(item.DisplayName);
        self.IsUser = ko.observable(item.MemberId == viewerId);

        self.IsAuthenticated = (viewerId > 0);
        self.ViewerUserId = ko.observable(viewerId);
        //Friend Observables
        self.FriendStatus = ko.observable(item.FriendStatus);
        self.FriendId = ko.observable(item.FriendId);
        self.ConnectionRequestdate = ko.observable(item.ConnectionRequestdate);


        //Computed Profile Observables
        self.City = ko.observable(item.City);
        self.Title = ko.observable(item.Title);
        self.Country = ko.observable(item.Country);
        self.Phone = ko.observable(item.Phone);
        self.Location = ko.observable(item.Location);
        self.Website = ko.observable(item.Website);
        self.PhotoURL = ko.observable(item.PhotoURL);
        self.ProfileProperties = ko.observable(item.ProfileProperties);
        self.ProfileUrl = ko.observable(item.ProfileUrl);
        //IsBlock: If viewer has been blocked by user
        // self.IsBlock = ko.observable(item.IsBlock);

        //IsViewerBlocked: If user has been blocked by Viewer
        //    self.IsViewerBlocked = ko.observable(item.IsViewerBlocked);



        self.HideBlockedSection = ko.computed(function () {

            return false;
        }, this);

        self.IsFriend = ko.computed(function () {
            return self.FriendStatus() == 2;
        }, this);

        self.IsPending = ko.computed(function () {
            return self.FriendStatus() == 1 && self.FriendId() != viewerId;
        }, this);

        self.HasPendingRequest = ko.computed(function () {

            return ((self.FriendStatus() == 1) && !self.IsPending());
        }, this);

        //Following Observables
        self.FollowingStatus = ko.observable(item.FollowingStatus);



        self.IsFollowing = ko.computed(function () {
            return self.FollowingStatus() == 2;
        }, this);

        //Follower Observables
        self.FollowerStatus = ko.observable(item.FollowerStatus);

        self.IsFollower = ko.computed(function () {
            return self.FollowerStatus() == 2;
        }, this);

        //Faviourate Obsservables

        self.FavStatus = ko.observable(item.FavStatus);
        self.IsFavorite = ko.observable(item.IsFavorite);



        self.IsOnlyFriend = ko.computed(function () {

            if (self.FriendStatus() == 2 && !self.IsFavorite())
                return true;
            return false;

        }, this);

        self.IsOnlyConnection = ko.computed(function () {


            if (self.FriendStatus() == 2)
                return true;
            return false;

        }, this);


        self.ShowRemoveConnection = ko.computed(function () {


            if (self.FriendStatus() == 2 && !self.IsFavorite())
                return true;
            return false;

        }, this);

        self.ConnectionAndFriend = ko.computed(function () {


            if (self.FriendStatus() == 2 && self.IsFavorite())
                return true;
            return false;

        }, this);
        self.IsSelfAccount = ko.computed(function () {
            if (self.UserId() == viewerId) return false;
            return true;
        }, this);

        //self.Location = ko.computed(function () {
        //    var city = self.City();
        //    var country = self.Country();
        //    var location = (city != null) ? city : '';
        //    if (location != '' && country != null && country != '') {
        //        location += ', ';
        //    }
        //    if (country != null) {
        //        location += country;
        //    }

        //    return location;
        //});

        self.getProfilePicture = function (w, h) {

            return profilePicHandler.replace("{0}", self.UserId()).replace("{1}", h).replace("{2}", w);
        };
        self.getToolToggle = function (w, h) {

            return profilePicHandler.replace("{0}", self.UserId()).replace("{1}", h).replace("{2}", w);
        };
        self.Profilelink = function () {

            return "/MyProfile/UserName/" + self.UserName();
        };
        self.ElementId = function (str) {

            return str + "_" + self.UserId();
        };
        self.ElementSymbol = function (str) {

            return "#" + str + "_" + self.UserId();
        };


        self.ConnectionMessage = function () {

            var data = "<br/>" + self.FirstName();
            if (self.JobTitle() != '')
                data = data + "<br/>" + self.JobTitle();

            if (self.RoleName() != '')
                data = data + "<br/>" + self.RoleName();
            if (self.CompanyName() != '')
                data = data + "<br/>" + self.CompanyName();

            data = data + "<br/> Request to Connect?";
            return data;
        };
        self.EmailFormat = function () {

            var data = "<br/>" + self.DisplayName();
            if (self.JobTitle() != '')
                data += "<br/>" + self.JobTitle();
            if (self.RoleName() != '')
                data += "<br/>" + self.RoleName();
            if (self.CompanyName() != '')
                data += "<br/>" + self.CompanyName();

            if (self.Location() != '')
                data += "<br/>" + self.Location();

            if (self.Phone() != '')
                data += "<br/>" + self.Phone();
            if (self.Email() != '')
                data += "<br/>" + self.Email();
            return data;
        };

        self.GetPrivateMessage = ko.observable(item.GetPrivateMessage);

        self.ConversionMessagae = function () {

            if (viewerId == self.UserId()) {
                $('#Messsagepopup_' + self.UserId()).hide();
                return "";
            }
            var omsgLst = self.GetPrivateMessage();
            var strMsg = "";
            if (omsgLst != null) {
                for (var i = 0; i < self.GetPrivateMessage().length; i++) {

                    var message = self.GetPrivateMessage()[i];

                    if (message != null) {

                        if (message.SenderUserID == viewerId)
                            strMsg += "You (<small>" + message.DisplayDateTime + "</small>) &nbsp;:&nbsp;&nbsp;";
                        else
                            strMsg += "<b>" + message.From + "</b> (<small>" + message.DisplayDateTime + "</small>) &nbsp;:&nbsp;&nbsp;";

                        strMsg += (message.Body != null ? message.Body.replace("<Attach>", "...") : "");
                        //if (message.Body != null)
                        //{
                        //    if (message.Body.contains("<attach>")) {
                        //        var olink = message.Body.split("<attach>")[1];

                        //    }

                        //}

                        strMsg += "<br/>"
                    }
                }
            }
            if (strMsg == '') {
                return '<h4>' + self.DisplayName() + "</h4>";

            }
            return strMsg;
        };


        self.modalVisible = ko.observable(false);
        self.modalVisible_Remove = ko.observable(false);

        self.modalVisible_RemoveFav = ko.observable(false);
        self.modalVisible_Message = ko.observable(false);
        self.modalVisible_Email = ko.observable(false);
        self.modalFriendVisible = ko.observable(false);
        self.AttachementVisible = ko.observable(true);
        self.AttachementVisible1 = ko.observable(true);
        self.modalIntermediate = ko.observable(false);
        self.modalVisible_EmailBox = ko.observable(false);
        self.modalVisible_MessageBox = ko.observable(false);



        self.show = function () {

            self.modalVisible(true);
            self.modalVisible_Remove(false);
            self.modalVisible_RemoveFav(false);
        };

        self.close = function () {
            self.modalVisible(false);
            self.modalVisible_Remove(false);

            self.modalVisible_RemoveFav(false);

            self.modalVisible(false);
            self.modalVisible_Message(false);
            self.modalVisible_Email(false);

            self.modalFriendVisible(false);
            self.modalVisible_EmailBox(false);
            self.modalVisible_MessageBox(false);

        };
        self.ShowRemove = function () {
            self.close();
            self.modalVisible_Remove(true);

        };
        self.CloseRemove = function () {
            self.modalVisible_Remove(false);
            self.modalVisible(false);
            self.modalVisible_Email(false);
            self.modalVisible_RemoveFav(false);
        };

        self.Message_Show = function () {
            self.close();
            self.modalVisible_Message(true);
            //   self.modalVisible_Email(false);
            //     self.modalVisible_RemoveFav(false); 
        };

        self.Message_Close = function () {
            self.close();
        };


        self.EmailBox_Show = function () {

            self.close();

            self.modalVisible_EmailBox(true);
        };

        self.MessageBox_Show = function () {


            $("#toMesssageBox1_" + self.UserId()).keyup(function (event) {
            //    alert(event.keyCode);
                if (event.keyCode === 13) {
                   // $("#tobutton1_" + self.UserId()).focus().click();
                    
                }
            });


            self.close();

            self.modalVisible_MessageBox(true);
            self.GetMessageList();
        };
        self.Email_Close = function () {
            self.close();
        };



        self.Email_Show = function () {
            //  if (self.IsViewerBlocked()) return;
            self.close();
            self.modalVisible_RemoveFav(true);


        };
        self.CloseRemoveFav = function () {

        };

        self.showfriend = function () {

            self.close();
            self.modalFriendVisible(true);
        };


        self.closefriend = function () {
            //   if (self.IsViewerBlocked()) return;
            self.close();
            self.modalFriendVisible(false);


        };


        self.ToolTipWindow = function (str) {
            $('#' + str).show();
        };
        //Actions
        self.acceptFriend = function () {

            //    if (self.IsViewerBlocked()) return;
           
            if (viewerId == self.UserId()) {
                AlertWindow('Warning', 'Sorry, You are trying to connect your own account.');
                self.modalVisible(false);
                return;
            }


            $.ajax({
                type: "POST",
                cache: false,
                url: baseServicepath + 'AcceptFriend',
                beforeSend: serviceFramework.setModuleHeaders,
                data: { friendId: self.UserId }
            }).done(function (data) {
                if (data.Result === "success") {
                    self.FriendStatus(2);
                } else {
                    displayMessage(settings.serverErrorText, "dnnFormWarning");
                }
            }).fail(function (xhr, status) {
                displayMessage(settings.serverErrorWithDescriptionText + status, "dnnFormWarning");
            });
        };

        self.addFriendConnection = function ()
        {

          //  alert(event.type);
            //if (window.KEY_RETURN == 13)
            //    return;
             
            if (viewerId == self.UserId())
            {
                AlertWindow('Warning', 'Sorry, You are trying to connect your own account.');
                self.modalVisible(false);
                return;
            }
           
            // self.modalIntermediate(true);
            $("#loading").show();
            $.ajax({
                type: "POST",
                cache: false,
                url: baseServicepath + 'AddFriend',
                beforeSend: serviceFramework.setModuleHeaders,
                data: { friendId: self.UserId() }
            }).done(function (data) {
                if (data.Result === "success") {
                    self.FriendStatus(1);
                    self.FriendId(self.UserId());
                    $("#loading").hide();
                    SuccessWindow("Confirmation", 'Your request has been sent to '+self.DisplayName());
                } else {

                    AlertWindow('Warning', 'Something went wrong. Please do try later');
                }
            }).fail(function (xhr, status) {
                // displayMessage(settings.serverErrorWithDescript   ionText + status, "dnnFormWarning");
                AlertWindow('Warning', "Sorry. Connection b/w you and " + self.DisplayName() + " not allowed");
                $("#loading").hide();
            });
        };
        self.addfavourite = function () {

            if (viewerId == self.UserId()) {
                AlertWindow('Warning', 'Sorry, You are trying to connect your own account.');
                self.modalVisible(false);
                return;
            }
            $.ajax({
                type: "POST",
                cache: false,
                url: baseServicepath + 'AddFaviourate',
                beforeSend: serviceFramework.setModuleHeaders,
                data: { friendId: self.UserId }
            }).done(function (data) {
                if (data.Result === "success") {

                    SuccessWindow('Success', 'User has been been added as favorite connection');
                    self.IsFavorite(true);
                    self.close();
                } if (data.Result === "limitcount") {
                    // displayMessage("You already added maximum allowed in faviourate", "dnnFormWarning");
                    AlertWindow('Warning', 'You already added 5 favorite people.');
                    self.FavStatus(false);
                    self.modalFriendVisible(false);

                }
            }).fail(function (xhr, status) {
                displayMessage(settings.serverErrorWithDescriptionText + status, "dnnFormWarning");
            });
        };


        //self.BlockUser = function () {

        //    if (viewerId == self.UserId()) {
        //        AlertWindow('Warning', 'Sorry, You are trying to block your own account.');

        //        return;
        //    }
        //    debugger;
        //    $.ajax({
        //        type: "POST",
        //        cache: false,
        //        url: baseServicepath + 'BlockUser',
        //        beforeSend: serviceFramework.setModuleHeaders,
        //        data: { friendId: self.UserId }
        //    }).done(function (data) {
        //        if (data.Result === "success") {

        //            SuccessWindow('Success', 'User has been been blocked.');
        //            self.IsBlock(true);
        //            self.close();
        //        } if (data.Result === "limitcount") {
        //            // displayMessage("You already added maximum allowed in faviourate", "dnnFormWarning");
        //            AlertWindow('Warning', 'You already added 5 favorite people.');
        //            self.IsBlock(true);
        //            self.modalFriendVisible(false);

        //        }
        //    }).fail(function (xhr, status) {
        //        alert(xhr);
        //        displayMessage(settings.serverErrorWithDescriptionText + status, "dnnFormWarning");
        //    });
        //};


        self.removefavourite = function () {

            if (viewerId == self.UserId()) {
                AlertWindow('Warning', 'Sorry, You are trying to connect your own account.');
                self.modalVisible(false);
                return;
            }
            $.ajax({
                type: "POST",
                cache: false,
                url: baseServicepath + 'RemoveFaviourate',
                beforeSend: serviceFramework.setModuleHeaders,
                data: { friendId: self.UserId }
            }).done(function (data) {
                if (data.Result === "success") {

                    SuccessWindow('Success', 'User has been removed from favorite connection');
                    self.FavStatus(false);
                    self.IsFavorite(false);
                    self.modalFriendVisible(false);
                    self.close();
                } else {
                    displayMessage(settings.serverErrorText, "dnnFormWarning");
                }
            }).fail(function (xhr, status) {
                displayMessage(settings.serverErrorWithDescriptionText + status, "dnnFormWarning");
            });
        };
        self.follow = function () {
            $.ajax({
                type: "POST",
                cache: false,
                url: baseServicepath + 'Follow',
                beforeSend: serviceFramework.setModuleHeaders,
                data: { followId: self.UserId }
            }).done(function (data) {
                if (data.Result === "success") {
                    self.FollowingStatus(2);
                } else {
                    displayMessage(settings.serverErrorText, "dnnFormWarning");
                }
            }).fail(function (xhr, status) {
                displayMessage(settings.serverErrorWithDescriptionText + status, "dnnFormWarning");
            });
        };

        self.RemoveFriend = function () {

            $.ajax({
                type: "POST",
                cache: false,
                url: baseServicepath + 'RemoveFriend',
                beforeSend: serviceFramework.setModuleHeaders,
                data: { friendId: self.UserId }
            }).done(function (data) {
                if (data.Result === "success") {
                    self.FriendStatus(0);
                    self.IsFavorite(false);
                    self.close();
                } else {
                    displayMessage(settings.serverErrorText, "dnnFormWarning");
                }
            }).fail(function (xhr, status) {
                displayMessage(settings.serverErrorWithDescriptionText + status, "dnnFormWarning");
            });
        };




        self.AttachFile = function addFile() {
            var fsClient = filestack.init(oKey);
            fsClient.pick({}).then(function (response) {

                response.filesUploaded.forEach(function (file) {
                    var ofileresponse = file;
                    var url = ofileresponse.url;


                    var hdnValue = $('#tohdnAttachment_' + self.UserId());
                    hdnValue.val(url);
                    var spanfileName = $('#spanfileName_' + self.UserId());
                    spanfileName.text(ofileresponse.filename);
                    self.AttachementVisible(false);

                });

            });

        }

        self.AttachFileMessage = function addFile() {
            var fsClient = filestack.init(oKey);
            fsClient.pick({}).then(function (response) {

                response.filesUploaded.forEach(function (file) {
                    var ofileresponse = file;
                    var url = ofileresponse.url;


                    var hdnValue = $('#tohdnAttachment1_' + self.UserId());
                    hdnValue.val(url);
                    var spanfileName = $('#spanfileName1_' + self.UserId());
                    spanfileName.html(ofileresponse.filename);
                    self.AttachementVisible1(false);

                });

            });

        }

        self.SendEmail = function () {
            if (viewerId == self.UserId()) {
                AlertWindow('Warning', 'Sorry, You are trying to send email to your own account.');
                self.modalVisible_EmailBox(false);
                return;
            }
            var sSubject = $('#tosubject_' + self.UserId()).val();
            var sBody = $('#toMesssage_' + self.UserId()).val();
            if (sSubject == '' || sBody.trim() == '') {
                $('#tosubject_' + self.UserId()).focus();
                AlertWindow('Warning', 'Sorry!! subject/body is required field.');

                return;
            }
            var uid = self.UserId();
            var sAttachFileName = $('#spanfileName_' + self.UserId()).html();
            var sFileStackLink = $('#tohdnAttachment_' + self.UserId()).val();
            

            $.confirm({
                title: 'Sending Email....',
               
                content: function () {
                    var self = this;
                    return $.ajax({
                        type: "GET",
                        cache: false,
                        url: baseServicepath + 'SendEmail',
                        beforeSend: serviceFramework.setModuleHeaders,
                        data: {
                            FromUserID: viewerId,
                            ToUserID: uid,
                            Subject: sSubject,
                            Body: sBody,
                            Filename: sAttachFileName,
                            AttachLink: sFileStackLink
                        }
                    }).done(function (data) {
                        if (data.Result === "success") {
                            //     displayMessage('Your Email has been sent successfully to: ' + self.DisplayName(), "dnnSuccess");
                            $('#tosubject_' + uid).val('');
                            $('#toMesssage_' + uid).val('');

                            $('#loading').hide();
                            SuccessWindow('Confirmation', 'Your email has been sent.');
                            self.close();
                            $('#showProgress_' + uid).html('');

                        } else {
                            AlertWindow('Error', 'Something went wrong.');
                            $('#loading').hide();
                        }
                    }).fail(function (xhr, status) {
                        //  displayMessage(settings.serverErrorWithDescriptionText + status, "dnnFormWarning");
                        $('#loading').hide();
                    });
                }
            });

            self.modalVisible_EmailBox(false);

        };


        self.SendMessage = function ()
        {

            var sSubject = "Private Message";

            if (viewerId == self.UserId()) {
                AlertWindow('Warning', 'Sorry, You are trying to send message to your own account.');
                self.modalVisible_MessageBox(false);
                return false;
            }
            var sBody = $('#toMesssageBox1_' + self.UserId()).val();
            if (sBody.trim() == '') {
               
                AlertWindow('Warning', 'Sorry!!  Message text is required field.');
                $('#toMesssageBox1_' + self.UserId()).focus();
                return false;
            }

            var sAttachFileName = $('#spanfileName1_' + self.UserId()).html();
            var sFileStackLink = $('#tohdnAttachment1_' + self.UserId()).val();

            //      $('#loading').show();
            $.ajax({
                type: "GET",
                cache: false,
                url: baseServicepath + 'SendMessage',
                beforeSend: serviceFramework.setModuleHeaders,
                data: {
                    FromUserID: viewerId,
                    ToUserID: self.UserId(),
                    Subject: sSubject,
                    Body: sBody,
                    Filename: sAttachFileName,
                    AttachLink: sFileStackLink
                }
            }).done(function (data) {
                if (data.Result === "success") {
                    $("#toMesssageBox1_" + self.UserId()).val('');

                    $("#spanfileName1_" + self.UserId()).html('');
                    $("#AttachementVisible1_" + self.UserId()).show();
                    $('#tohdnAttachment1_' + self.UserId()).val('');
                    self.GetMessageList();
                    $('#loading').hide();
                    //  self.modalVisible_MessageBox(false);
                } else {
                    displayMessage('Something went wrong', "dnnFormWarning");
                    $('#loading').hide();
                }
            }).fail(function (xhr, status) {
                displayMessage(settings.serverErrorWithDescriptionText + status, "dnnFormWarning");
                $('#loading').hide();
            });
        };


        self.GetMessageList = function () {


            $.ajax({
                type: "GET",
                cache: false,
                url: baseServicepath + 'GetMessage',
                beforeSend: serviceFramework.setModuleHeaders,
                data: { friendId: self.UserId() }
            }).done(function (member) {



                self.GetPrivateMessage = ko.observable(member.GetPrivateMessage);
                var data = self.ConversionMessagae();
                $("#spanConversion_" + self.UserId()).html('');
                $("#spanConversion_" + self.UserId()).html(data);
                $("#toMesssageBox1_" + self.UserId()).focus();
            }).fail(function (xhr, status) {


                //  response({});
            });
        };

        self.unFollow = function () {
            $.ajax({
                type: "POST",
                cache: false,
                url: baseServicepath + 'UnFollow',
                beforeSend: serviceFramework.setModuleHeaders,
                data: { followId: self.UserId }
            }).done(function (data) {
                if (data.Result === "success") {
                    self.FollowingStatus(0);
                } else {
                    displayMessage(settings.serverErrorText, "dnnFormWarning");
                }
            }).fail(function (xhr, status) {
                displayMessage(settings.serverErrorWithDescriptionText + status, "dnnFormWarning");
            });
        };
    };
  
    function CADirectoryViewModel(initialData) {
        // Data
        var self = this;

        var initialMembers = $.map(initialData, function (item) { return new Member(item); });

        self.Visible = ko.observable(true);
        self.Members = ko.observableArray(initialMembers);
        //var NumberofRecord = initialMembers.length;
        self.CanLoadMore = ko.observable((initialMembers.length) == pageSize);
        self.SearchTerm = ko.observable('');


        self.disablePrivateMessage = ko.observable(settings.disablePrivateMessage);

        self.ResetEnabled = ko.observable(false);
        self.totalRecord = ko.observable(settings.Total);
        self.HasMembers = ko.computed(function () {
            return self.Members().length > 0;
        }, this);

        self.AdvancedSearchTerm1 = ko.observable('');
        self.AdvancedSearchTerm2 = ko.observable('');
        self.AdvancedSearchTerm3 = ko.observable('');
        self.AdvancedSearchTerm4 = ko.observable('');

        self.LastSearch = ko.observable('Advanced');

        self.loadingData = ko.observable(false);

        //Action Methods
        self.advancedSearch = function () {
            pageIndex = 0;
            self.SearchTerm('');

            self.xhrAdvancedSearch();
            self.LastSearch('Advanced');
            self.ResetEnabled(true);
        };

        self.basicSearch = function () {
            pageIndex = 0;
            self.AdvancedSearchTerm1('');
            self.AdvancedSearchTerm2('');
            self.AdvancedSearchTerm3('');
            self.AdvancedSearchTerm4('');

            self.xhrBasicSearch();

            self.LastSearch('Basic');
            self.ResetEnabled(true);
        };

        self.getMember = function (item) {

            self.SearchTerm(item.value);
            $.ajax({
                type: "GET",
                cache: false,
                url: baseServicepath + "GetMember",
                beforeSend: serviceFramework.setModuleHeaders,
                data: {
                    userId: item.userId,
                    SortBy: OrderBy
                }
            }).done(function (members) {

                if (typeof members !== "undefined" && members != null) {

                    var mappedMembers = $.map(members, function (member) { return new Member(member); });
                    self.Members(mappedMembers);
                    self.CanLoadMore(false);

                } else {
                    displayMessage(settings.serverErrorText, "dnnFormWarning");
                }
            }).fail(function (xhr, status) {
                displayMessage(settings.serverErrorWithDescriptionText + status, "dnnFormWarning");
            });
        };

        self.getSuggestions = function (term, response) {
            $.ajax({
                type: "GET",
                cache: false,
                url: baseServicepath + "GetSuggestions",
                beforeSend: serviceFramework.setModuleHeaders,
                data: {
                    groupId: groupId,
                    displayName: term
                }
            }).done(function (data) {
                response(data);
            }).fail(function () {
                displayMessage(settings.searchErrorText, "dnnFormWarning");
                response({}); // From jQuery UI docs: You must always call the response callback even if you encounter an error
            });
        };

        self.handleAfterRender = function (elements, data) {
            for (var i in elements) {
                var element = elements[i];
                if (element.nodeType == 1) {
                    var config = {
                        over: function () {
                            $(this).find("div[id$='popUpPanel']").fadeIn("slow");
                        },
                        timeout: 500,
                        out: function () {
                            $(this).find("div[id$='popUpPanel']").fadeOut("fast");
                        }
                    };
                    $(element).hoverIntent(config);
                }
            }
            self.CanLoadMore(true);

        };

        self.isEven = function (item) {
            return (self.Members.indexOf(item) % 2 == 0);
        };

        self.loadMore = function () {

            $('#hyperloading').html('<img src="/images/icon_wait.gif" width="16px" height="16px"></img> Loading...');
            // $('#hyperloading').offset();
            // setTimeout($('html, body').animate({scrollTop: $('#hyperloading').offset().top - 20}, 500), 1000);
            pageIndex++;
            if (self.LastSearch() === 'Advanced') {
                self.xhrAdvancedSearch();
            }
            else {
                self.xhrBasicSearch();
            }
             
            //$('html, body').animate({
            //    scrollTop: $("#loadMore").offset().top
            //}, 500);
        };


        self.DoSort = function () {
            
            self.loadingData(true);
            var conceptVal = $("#sortby option").filter(":selected").val();
            pageIndex = 0;

            OrderBy = conceptVal;
            if (self.LastSearch() === 'Advanced') {
                self.xhrAdvancedSearch();
            }
            else {
                self.xhrBasicSearch();
            }
           
        };


        self.DoFilter = function (index, event) {
            self.loadingData(true);
            var id = event.currentTarget.id;
            pageIndex = 0;
            iFilterBy = index;
            if (self.LastSearch() === 'Advanced') {
                self.xhrAdvancedSearch();
            }
            else {
                self.xhrBasicSearch();
            }
            CallMe(id);
            
        };

        self.resetSearch = function () {
            self.SearchTerm('');
            self.AdvancedSearchTerm1('');
            self.AdvancedSearchTerm2('');
            self.AdvancedSearchTerm3('');
            self.AdvancedSearchTerm4('');
            self.xhrAdvancedSearch();
            self.LastSearch('Advanced');
            self.ResetEnabled(false);
        };

        self.xhrAdvancedSearch = function () {
            
            $.ajax({
                type: "GET",
                cache: false,
                url: baseServicepath + "AdvancedSearch",
                beforeSend: serviceFramework.setModuleHeaders,
                data: {
                    userId: userId,
                    groupId: groupId,
                    pageIndex: pageIndex,
                    pageSize: pageSize,
                    searchTerm1: self.AdvancedSearchTerm1(),
                    searchTerm2: self.AdvancedSearchTerm2(),
                    searchTerm3: self.AdvancedSearchTerm3(),
                    searchTerm4: self.AdvancedSearchTerm4(),
                    SortBy: OrderBy,
                    TotalCount: tCount,
                    ViewType: iViewType,
                    FilterBy: iFilterBy,

                }
            }).done(function (members) {


                if (typeof members !== "undefined" && members != null) {
                    var mappedMembers = $.map(members, function (item) { return new Member(item); });

                    if (pageIndex === 0) {
                        self.Members(mappedMembers);
                    }
                    else {
                        for (var i = 0; i < mappedMembers.length; i++) {
                            self.Members.push(mappedMembers[i]);
                        }
                    }
                    $('#hyperloading').html('&darr;  Load More..');
                    self.CanLoadMore(mappedMembers.length == pageSize);
                    
                } else {
                    displayMessage(settings.searchErrorText, "dnnFormWarning");
                }

                self.loadingData(false);
            }).fail(function (qaas) {
                displayMessage(settings.searchErrorText, "dnnFormWarning");
                response({});
            });
        };


        self.xhrBasicSearch = function () {

            $.ajax({
                type: "GET",
                cache: false,
                url: baseServicepath + "BasicSearch",
                beforeSend: serviceFramework.setModuleHeaders,
                data: {
                    groupId: groupId,
                    searchTerm: self.SearchTerm(),
                    pageIndex: pageIndex,
                    pageSize: pageSize,
                    SortBy: OrderBy,
                    TotalCount: tCount,
                    ViewType: iViewType,
                    FilterBy: iFilterBy,
                }
            }).done(function (members) {

                if (typeof members !== "undefined" && members != null) {
                    var mappedMembers = $.map(members, function (item) { return new Member(item); });
                    // self.totalRecord = totalRecord;
                    if (pageIndex === 0) {
                        self.Members(mappedMembers);
                    } else {
                        for (var i = 0; i < mappedMembers.length; i++) {
                            self.Members.push(mappedMembers[i]);
                        }
                    }
                    $('#hyperloading').html('&darr;  Load More..');
                    self.CanLoadMore(mappedMembers.length == pageSize);
                    
                    self.loadingData(false);
                } else {
                    displayMessage(settings.searchErrorText, "dnnFormWarning");
                }
            }).fail(function (xhr, status) {
                displayMessage(settings.searchErrorText, "dnnFormWarning");
                response({});
            });
        };

        $(window).scroll(function () {
            if ($(window).scrollTop() + $(window).height() == $(document).height()) {
                 self.loadMore();
             }
        });
       
    };

    this.init = function (element) {
        containerElement = element;
    
        //load initial state of inbox
        $.ajax({
            type: "GET",
            cache: false,
            url: baseServicepath + "AdvancedSearch",
            beforeSend: serviceFramework.setModuleHeaders,
            data: {
                userId: userId,
                groupId: groupId,
                pageIndex: pageIndex,
                pageSize: pageSize,
                searchTerm1: '',
                searchTerm2: '',
                searchTerm3: '',
                searchTerm4: '',
                SortBy: OrderBy,
                TotalCount: tCount,
                ViewType: iViewType,
                FilterBy: iFilterBy,
            }
        }).done(function (members) {

            if (typeof members !== "undefined" && members != null) {
                var viewModel = new CADirectoryViewModel(members);
                ko.applyBindings(viewModel, document.getElementById($(element).attr("id")));

                //Basic Search
                $('#mdBasicSearch').autocomplete({
                    source: function (request, response) {
                        viewModel.getSuggestions(request.term, response);
                        return;
                    },
                    minLength: 3,
                    select: function (event, ui) {
                        viewModel.getMember(ui.item);
                    }
                });

                //Advanced Search
                $('a#mdAdvancedSearch').click(function (event) {
                    event.preventDefault();
                    $('div#mdAdvancedSearchForm').slideDown();
                    $(this).addClass("active");
                    $(".mdSearch").addClass("active");
                });

                var timer;
                var cursorIsOnAdvancedSearchForm;
                //$('a#mdAdvancedSearch').mouseleave(function () {
                //    timer = setTimeout(function () {
                //        if ($('div#mdAdvancedSearchForm').is(':visible') && !cursorIsOnAdvancedSearchForm) {
                //            $('div#mdAdvancedSearchForm').hide();
                //            $(this).removeClass("active");
                //            $(".mdSearch").removeClass("active");
                //        }
                //    }, 150);

                //});
                //$('div#mdAdvancedSearchForm').mouseenter(function () {
                //    cursorIsOnAdvancedSearchForm = true;
                //});
                //$('div#mdAdvancedSearchForm').mouseleave(function () {
                //    clearTimeout(timer);
                //    cursorIsOnAdvancedSearchForm = false;
                //    $(this).hide();
                //    $('a#mdAdvancedSearch').removeClass("active");
                //    $(".mdSearch").removeClass("active");
                //});
              
                //Compose Message
                if (!settings.disablePrivateMessage) {
                    var options = $.extend({}, {
                        openTriggerSelector: containerElement + " .ComposeMessage",
                        onPrePopulate: function (target) {
                            var context = ko.contextFor(target);
                            var prePopulatedRecipients = [{ id: "user-" + context.$data.UserId(), name: context.$data.DisplayName() }];
                            return prePopulatedRecipients;
                        },
                        servicesFramework: serviceFramework
                    }, composeMessageSettings);
                    $.fn.dnnComposeMessage(options);
                }
            } else {
                displayMessage(settings.serverErrorText, "dnnFormWarning");
            }
        }).fail(function (xhr, status) {
            displayMessage(settings.serverErrorWithDescriptionText + status, "dnnFormWarning");
        });
    };

}

CADirectory.defaultSettings = {
    userId: -1,
    groupId: -1,
    viewerId: -1,
    profileUrl: "",

    profileUrlUserToken: "PROFILEUSER",
    addFriendText: "AddFriend",
    acceptFriendText: "AcceptFriend",
    friendPendingText: "FriendPendingText",
    removeFriendText: "RemoveFriendText",
    followText: "FollowText",
    unFollowText: "UnFollowText",
    sendMessageText: "SendMessageText",
    userNameText: "UserNameText",
    emailText: "EmailText",
    cityText: "CityText",
    serverErrorText: "ServerErrorText",
    serverErrorWithDescriptionText: "ServerErrorWithDescriptionText"
};

