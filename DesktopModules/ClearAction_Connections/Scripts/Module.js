function koConnectionAlert($, ko, settings, composeMessageSettings) {

    var opts = $.extend({}, koConnectionAlert.defaultSettings, settings);
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

        var selfAlert = this;

        selfAlert.AddFriendText = opts.addFriendText;
        selfAlert.AcceptFriendText = opts.acceptFriendText;
        selfAlert.FriendPendingText = opts.friendPendingText;
        selfAlert.RemoveFriendText = opts.removeFriendText;
        selfAlert.FollowText = opts.followText;
        selfAlert.UnFollowText = opts.unFollowText;
        selfAlert.SendMessageText = opts.sendMessageText;
        selfAlert.UserNameText = opts.userNameText;
        selfAlert.EmailText = opts.emailText;
        selfAlert.CityText = opts.cityText;

        selfAlert.RoleName = ko.observable(item.RoleName);
        selfAlert.CompanyName = ko.observable(item.CompanyName);
        selfAlert.JobTitle = ko.observable(item.JobTitle);
        selfAlert.ToolTipId = '#tooltip' + ko.observable(item.UserId);
        selfAlert.UserId = ko.observable(item.MemberId);
        selfAlert.LastName = ko.observable(item.LastName);
        selfAlert.FirstName = ko.observable(item.FirstName);
        selfAlert.UserName = ko.observable(item.UserName);
        selfAlert.DisplayName = ko.observable(item.DisplayName);
        selfAlert.Email = ko.observable(item.Email);
        selfAlert.sortMode = ko.observable(item.DisplayName);
        selfAlert.IsUser = ko.observable(item.MemberId == viewerId);

        selfAlert.IsAuthenticated = (viewerId > 0);
        selfAlert.ViewerUserId = ko.observable(viewerId);
        //Friend Observables
        selfAlert.FriendStatus = ko.observable(item.FriendStatus);
        selfAlert.FriendId = ko.observable(item.FriendId);



        //Computed Profile Observables
        selfAlert.City = ko.observable(item.City);
        selfAlert.Title = ko.observable(item.Title);
        selfAlert.Country = ko.observable(item.Country);
        selfAlert.Phone = ko.observable(item.Phone);
        selfAlert.Location = ko.observable(item.Location);
        selfAlert.Website = ko.observable(item.Website);
        selfAlert.PhotoURL = ko.observable(item.PhotoURL);
        selfAlert.ProfileProperties = ko.observable(item.ProfileProperties);
        selfAlert.ProfileUrl = ko.observable(item.ProfileUrl);
        selfAlert.ConnectionRequestdate = ko.observable(item.ConnectionRequestdate);
        

        selfAlert.HideBlockedSection = ko.computed(function () {

            return false;
        }, this);

        selfAlert.IsFriend = ko.computed(function () {
            return selfAlert.FriendStatus() == 2;
        }, this);

        selfAlert.IsPending = ko.computed(function () {
            return selfAlert.FriendStatus() == 1;
        }, this);

        selfAlert.HasPendingRequest = ko.computed(function () {

            return ((selfAlert.FriendStatus() == 1) && !selfAlert.IsPending());
        }, this);

        //Following Observables
        selfAlert.FollowingStatus = ko.observable(item.FollowingStatus);



        selfAlert.IsFollowing = ko.computed(function () {
            return selfAlert.FollowingStatus() == 2;
        }, this);

        //Follower Observables
        selfAlert.FollowerStatus = ko.observable(item.FollowerStatus);

        selfAlert.IsFollower = ko.computed(function () {
            return selfAlert.FollowerStatus() == 2;
        }, this);

        //Faviourate Obsservables

        selfAlert.FavStatus = ko.observable(item.FavStatus);
        selfAlert.IsFavorite = ko.observable(item.IsFavorite);

        selfAlert.Connected = ko.computed(function () {
            if (selfAlert.FollowerStatus() == 2) return "Connected";
            return "Waiting..";
        }, this);

        selfAlert.IsOnlyFriend = ko.computed(function () {

            if (selfAlert.FriendStatus() == 2 && !selfAlert.IsFavorite())
                return true;
            return false;

        }, this);



        selfAlert.ShowRemoveConnection = ko.computed(function () {


            if (selfAlert.FriendStatus() == 2 && !selfAlert.IsFavorite())
                return true;
            return false;

        }, this);

        selfAlert.Connection = ko.computed(function () {


            if (selfAlert.FriendStatus() == 2)
                return true;
            return false;

        }, this);
        selfAlert.IsSelfAccount = ko.computed(function () {
            if (selfAlert.UserId() == viewerId) return false;
            return true;
        }, this);

       

        selfAlert.getProfilePictureAlert = function (w, h) {

            return profilePicHandler.replace("{0}", selfAlert.UserId()).replace("{1}", h).replace("{2}", w);
        };
        selfAlert.getToolToggle = function (w, h) {

            return profilePicHandler.replace("{0}", selfAlert.UserId()).replace("{1}", h).replace("{2}", w);
        };
        selfAlert.Profilelink = function () {

            return "/MyProfile/UserName/" + selfAlert.UserName();
        };
        selfAlert.ElementIdAlert = function (str) {

            return str + "_" + selfAlert.UserId();
        };
        selfAlert.ElementSymbolAlert = function (str) {

            return "#" + str + "_" + selfAlert.UserId();
        };



        
        //Actions
        selfAlert.acceptFriendAlert = function () {
            
            //    if (selfAlert.IsViewerBlocked()) return;
            $("#spaninfoAlert_" + selfAlert.UserId()).html('Processing...');
            if (viewerId == selfAlert.UserId()) {
                AlertWindow('Warning', 'Sorry, You are trying to connect your own account.');
                selfAlert.modalVisible(false);
                return;
            }
            var o = selfAlert;

            $.ajax({
                type: "POST",
                cache: false,
                url: baseServicepath + 'AcceptFriend',
                beforeSend: serviceFramework.setModuleHeaders,
                data: { friendId: selfAlert.UserId }
            }).done(function (data) {
                if (data.Result === "success") {
                    
                    o.FriendStatus = 2;
                    $("#spaninfoAlert_" + o.UserId()).html('Connected.');
                    $("#btnAcceptAlert_" + o.UserId()).hide();
                    $("#btndeclineAlert_" + o.UserId()).hide();

                    alert('Success! Your are now in connection.');
                    
                    window.location.href = location.href;
                }
                else {
                    displayMessage(settings.serverErrorText, "dnnFormWarning");
                }
            }).fail(function (xhr, status) {
                displayMessage(settings.serverErrorWithDescriptionText + status, "dnnFormWarning");
            });
        };


        selfAlert.RemoveFriendAlert = function () {

            $.ajax({
                type: "POST",
                cache: false,
                url: baseServicepath + 'RemoveFriend',
                beforeSend: serviceFramework.setModuleHeaders,
                data: { friendId: selfAlert.UserId }
            }).done(function (data) {
                if (data.Result === "success") {
                    alert('Success! Request has been removed.');
                    // AlertWindow('Confirmation', 'You are now connected with ' + selfAlert.FirstName);
                    window.location.href = location.href;
                } else {
                    displayMessage(settings.serverErrorText, "dnnFormWarning");
                }
            }).fail(function (xhr, status) {
                displayMessage(settings.serverErrorWithDescriptionText + status, "dnnFormWarning");
            });
        };

        //selfAlert.unFollowAlert = function () {
        //    $.ajax({
        //        type: "POST",
        //        cache: false,
        //        url: baseServicepath + 'UnFollow',
        //        beforeSend: serviceFramework.setModuleHeaders,
        //        data: { followId: selfAlert.UserId }
        //    }).done(function (data) {
        //        if (data.Result === "success") {
        //            selfAlert.FollowingStatus(0);
        //        } else {
        //            displayMessage(settings.serverErrorText, "dnnFormWarning");
        //        }
        //    }).fail(function (xhr, status) {
        //        displayMessage(settings.serverErrorWithDescriptionText + status, "dnnFormWarning");
        //    });
        //};
    };

    function ConnectionAlertViewModel(initialData) {
        // Data
        var selfAlert = this;

        var initialMembers = $.map(initialData, function (item) { return new Member(item); });

        selfAlert.Visible = ko.observable(true);
        selfAlert.Members = ko.observableArray(initialMembers);
        //var NumberofRecord = initialMembers.length;
        selfAlert.CanLoadMore = ko.observable((initialMembers.length) == pageSize);
        selfAlert.SearchTerm = ko.observable('');


        selfAlert.disablePrivateMessage = ko.observable(settings.disablePrivateMessage);

        selfAlert.ResetEnabled = ko.observable(false);
        selfAlert.totalRecord = ko.observable(settings.Total);
        selfAlert.HasMembers = ko.computed(function () {
            return selfAlert.Members().length > 0;
        }, this);

        selfAlert.AdvancedSearchTerm1 = ko.observable('');
        selfAlert.AdvancedSearchTerm2 = ko.observable('');
        selfAlert.AdvancedSearchTerm3 = ko.observable('');
        selfAlert.AdvancedSearchTerm4 = ko.observable('');

        selfAlert.LastSearch = ko.observable('Advanced');

        selfAlert.loadingData = ko.observable(false);

        //Action Methods
        selfAlert.advancedSearch = function () {
            pageIndex = 0;
            selfAlert.SearchTerm('');

            selfAlert.xhrAdvancedSearchAlert();
            selfAlert.LastSearch('Advanced');
            selfAlert.ResetEnabled(true);
        };

        selfAlert.basicSearchAlert = function () {
            pageIndex = 0;
            selfAlert.AdvancedSearchTerm1('');
            selfAlert.AdvancedSearchTerm2('');
            selfAlert.AdvancedSearchTerm3('');
            selfAlert.AdvancedSearchTerm4('');

            selfAlert.xhrbasicSearchAlert();

            selfAlert.LastSearch('Basic');
            selfAlert.ResetEnabled(true);
        };

        //selfAlert.getMember = function (item) {

        //    selfAlert.SearchTerm(item.value);
        //    $.ajax({
        //        type: "GET",
        //        cache: false,
        //        url: baseServicepath + "GetMember",
        //        beforeSend: serviceFramework.setModuleHeaders,
        //        data: {
        //            userId: item.userId,
        //            SortBy: OrderBy
        //        }
        //    }).done(function (members) {

        //        if (typeof members !== "undefined" && members != null) {
        //            $('#requestcount').html(members.length);
        //            var mappedMembers = $.map(members, function (member) { return new Member(member); });
        //            selfAlert.Members(mappedMembers);
        //            selfAlert.CanLoadMore(false);

        //        } else {
        //            displayMessage(settings.serverErrorText, "dnnFormWarning");
        //        }
        //    }).fail(function (xhr, status) {
        //        displayMessage(settings.serverErrorWithDescriptionText + status, "dnnFormWarning");
        //    });
        //};



        selfAlert.handleAfterRender = function (elements, data) {
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
        };

        selfAlert.isEven = function (item) {
            return (selfAlert.Members.indexOf(item) % 2 == 0);
        };

        selfAlert.loadMore = function () {

            pageIndex++;

            if (selfAlert.LastSearch() === 'Advanced') {
                selfAlert.xhrAdvancedSearchAlertAlert();
            }
            else {
                selfAlert.xhrbasicSearchAlert();
            }

        };


        selfAlert.DoSortAlert = function () {
            debugger;
            var conceptVal = $("#sortbyAlert option").filter(":selected").val();
            pageIndex = 0;

            OrderBy = conceptVal;
            selfAlert.xhrAdvancedSearchAlert();

        };


        selfAlert.DoFilter = function (index, event) {
            selfAlert.loadingData(true);

            var id = event.currentTarget.id;



            pageIndex = 0;
            iFilterBy = index;

            selfAlert.xhrAdvancedSearchAlert();
            //  selfAlert.loadingData(false);
        };




        selfAlert.resetSearchAlert = function () {
            selfAlert.SearchTerm('');
            selfAlert.AdvancedSearchTerm1('');
            selfAlert.AdvancedSearchTerm2('');
            selfAlert.AdvancedSearchTerm3('');
            selfAlert.AdvancedSearchTerm4('');

            selfAlert.xhrAdvancedSearchAlert();
            selfAlert.LastSearch('Advanced');
            selfAlert.ResetEnabled(false);
        };

        selfAlert.xhrAdvancedSearchAlert = function () {

            $.ajax({
                type: "GET",
                cache: false,
                url: baseServicepath + "GetConnectionRequest",
                beforeSend: serviceFramework.setModuleHeaders,
                data: {

                    SortBy: OrderBy,


                }
            }).done(function (members) {


                if (typeof members !== "undefined" && members != null) {
                    var mappedMembers = $.map(members, function (item) { return new Member(item); });
                    $("#spanCounter").html(members.length);
                    $("#requestcount").html(members.length);

                    if (pageIndex === 0) {
                        selfAlert.Members(mappedMembers);
                    }
                    else {
                        for (var i = 0; i < mappedMembers.length; i++) {
                            selfAlert.Members.push(mappedMembers[i]);
                        }
                    }
                    selfAlert.CanLoadMore(mappedMembers.length == pageSize);

                } else {
                    displayMessage(settings.searchErrorText, "dnnFormWarning");
                }

                selfAlert.loadingData(false);
            }).fail(function (qaas) {
                displayMessage(settings.searchErrorText, "dnnFormWarning");
                response({});
            });
        };


        selfAlert.xhrbasicSearchAlert = function () {

            $.ajax({
                type: "GET",
                cache: false,
                url: baseServicepath + "basicSearchAlert",
                beforeSend: serviceFramework.setModuleHeaders,
                data: {
                    groupId: groupId,
                    searchTerm: selfAlert.SearchTerm(),
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
                    // selfAlert.totalRecord = totalRecord;


                    if (pageIndex === 0) {
                        selfAlert.Members(mappedMembers);
                    } else {
                        for (var i = 0; i < mappedMembers.length; i++) {
                            selfAlert.Members.push(mappedMembers[i]);
                        }
                    }
                    selfAlert.CanLoadMore(mappedMembers.length == pageSize);
                } else {
                    displayMessage(settings.searchErrorText, "dnnFormWarning");
                }
            }).fail(function (xhr, status) {
                displayMessage(settings.searchErrorText, "dnnFormWarning");
                response({});
            });
        };


    };

    this.init = function (element) {
        containerElement = element;

        //load initial state of inbox
        $.ajax({
            type: "GET",
            cache: false,
            url: baseServicepath + "GetConnectionRequest",
            beforeSend: serviceFramework.setModuleHeaders,
            data: {
                SortBy: OrderBy,

            }
        }).done(function (members) {

            if (typeof members !== "undefined" && members != null) {

                $("#spanCounter").html(members.length);
                $("#requestcount").html(members.length);
                var viewModel = new ConnectionAlertViewModel(members);
                ko.applyBindings(viewModel, document.getElementById($(element).attr("id")));

            } else {
                displayMessage(settings.serverErrorText, "dnnFormWarning");
            }
        }).fail(function (xhr, status) {
            displayMessage(settings.serverErrorWithDescriptionText + status, "dnnFormWarning");
        });
    };
}

koConnectionAlert.defaultSettings = {
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