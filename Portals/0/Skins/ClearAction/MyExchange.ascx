﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="DotNetNuke.UI.Skins.Skin" %>

<%@ Register TagPrefix="dnn" TagName="STYLES" Src="~/Admin/Skins/Styles.ascx" %>
<%@ Register TagPrefix="dnn" TagName="CURRENTDATE" Src="~/Admin/Skins/CurrentDate.ascx" %>
<%@ Register TagPrefix="dnn" TagName="LANGUAGE" Src="~/Admin/Skins/Language.ascx" %>
<%@ Register TagPrefix="dnn" TagName="LOGO" Src="~/Admin/Skins/Logo.ascx" %>
<%@ Register TagPrefix="dnn" TagName="SEARCH" Src="~/Admin/Skins/Search.ascx" %>
<%@ Register TagPrefix="dnn" TagName="USER" Src="~/Admin/Skins/User.ascx" %>
<%@ Register TagPrefix="dnn" TagName="LOGIN" Src="~/Admin/Skins/Login.ascx" %>
<%@ Register TagPrefix="dnn" TagName="PRIVACY" Src="~/Admin/Skins/Privacy.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TERMS" Src="~/Admin/Skins/Terms.ascx" %>
<%@ Register TagPrefix="dnn" TagName="BREADCRUMB" Src="~/Admin/Skins/BreadCrumb.ascx" %>
<%@ Register TagPrefix="dnn" TagName="COPYRIGHT" Src="~/Admin/Skins/Copyright.ascx" %>
<%@ Register TagPrefix="dnn" TagName="LINKTOMOBILE" Src="~/Admin/Skins/LinkToMobileSite.ascx" %>
<%@ Register TagPrefix="dnn" TagName="META" Src="~/Admin/Skins/Meta.ascx" %>
<%@ Register TagPrefix="dnn" TagName="MENU" Src="~/DesktopModules/DDRMenu/Menu.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<%@ Register TagPrefix="dnn" TagName="jQuery" Src="~/Admin/Skins/jQuery.ascx" %>


<dnn:META ID="META1" runat="server" Name="viewport" Content="width=device-width,initial-scale=1" />

<script src="<%=SkinPath%>js/jquery-1.8.2.min.js.download" type="text/javascript"></script>
<script src="<%=SkinPath%>js/redir.js.download" type="text/javascript"></script>

<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/bootstrap.css">
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/font-awesome.min.css">
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/aria-tooltip.css">
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/style.css">
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/responsive.css">
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/demo.css">
<script src="<%=SkinPath%>js/bootstrap.min.js"></script>
<div class="wrapper">
    <!-- wrapper_cnt starts -->
    <div class="wrapper_cnt">
        <div class="col-sm-12 main_wrapper">
            <header>
                <div class="header_lt col-sm-2 nopadding">
                    <div class="exchange_lt">
                        <dnn:LOGO ID="dnnLogo" runat="server" />

                    </div>
                </div>
                <div class="menu-toggle col-sm-10 nopadding">

                    <div class="header_rt col-sm-4 nopadding">
                        <ul>
                            <li>
                                <a href="../profile/setup1.html">
                                    <span>Profile</span>
                                    <figure>
                                        <img src="<%=SkinPath%>images/icon-profile-photo.png" width="23" height="23" alt="icon-profile" class="img-responsive">
                                    </figure>
                                </a>
                            </li>
                            <li class="demo">
                                <a href="#">
                                    <span>Alerts</span>
                                    <figure>
                                        <img src="<%=SkinPath%>images/icon-alerts.png" width="20" height="21" alt="icon-alerts" class="img-responsive">
                                    </figure>
                                    <div class="pos_1">
                                        <div class="tbl">
                                            <div class="tbl_cell">
                                                <small>88</small>
                                            </div>
                                        </div>
                                    </div>
                                </a>
                            </li>
                            <li class="demo">
                                <a href="#">
                                    <span>Contact</span>
                                    <figure>
                                        <img src="<%=SkinPath%>images/icon-contact.png" width="36" height="21" alt="icon-contact" class="img-responsive">
                                    </figure>
                                </a>
                            </li>
                            <li class="demo">
                                <a href="#">
                                    <span>Log Out</span>
                                    <figure>
                                        <img src="<%=SkinPath%>images/icon-logout.png" width="22" height="22" alt="icon-logout" class="img-responsive">
                                    </figure>
                                </a>
                            </li>
                        </ul>
                        <div class="clearfix"></div>
                    </div>
                    <div class="header_cnt col-sm-8 nopadding">
                        <div class="exchange_rt">
                            <nav>
                                <ul>
                                    <li class="active"><a href="my-exchange.html" class="exe1">My Exchange</a></li>
                                    <li><a href="my-vault.html" class="exe2">Solve-Spaces</a></li>
                                    <li><a href="../forum/open-forum.html" class="exe3">Open Forum</a></li>
                                    <li class="demo"><a href="#" class="exe12">Blog</a></li>
                                    <li class="demo"><a href="#" class="exe4">Events</a></li>
                                    <li class="demo"><a href="#" class="exe5">Value Vault</a></li>
                                    <li class="demo"><a href="#" class="exe7">Connections</a></li>
                                </ul>
                            </nav>
                            <dnn:MENU ID="MENU" MenuStyle="Menus/MainMenu" runat="server" NodeSelector="*"></dnn:MENU>
                            <div class="clearfix"></div>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>
                <div class="clearfix"></div>
            </header>
            <div class="menu">
                <a href="#"><i class="fa fa-bars" aria-hidden="true"></i></a>
            </div>
            <div class="clearfix"></div>
            <div class="cnt_wrapper my-exchange">
                <div class="ContentPane" runat="server" id="ContentPane"></div>
                <div class="cnt_wraper_lt col-sm-8">
                    <div class="intro-titles">
                        <h1 class="my-exchange-title">My Exchange</h1>
                        <a href="#">
                            <img src="<%=SkinPath%>images/img_17.png" width="14" height="14" alt="img_17"><span>Executive Report</span></a>
                    </div>
                    <div class="clearfix"></div>
                    <div class="analytics">
                        <h4>Analytics</h4>
                        <div class="analytics_dtls">
                            <div class="sec_title">
                                <h5>MY CONTRIBUTIONS</h5>
                            </div>
                            <div class="sec_charts">
                                <div class="expand">
                                    <a href="#">Expand</a>
                                    <div class="mob_expand hidden visible-xs"></div>
                                </div>
                                <div class="completed">
                                    <p>3<span>completed</span></p>
                                </div>
                                <div class="clearfix"></div>
                                <div class="interactions">
                                    <p>4<span>interactions</span></p>
                                </div>
                                <div class="clearfix"></div>
                                <div class="interactions1">
                                    <p>4<span>joined</span></p>
                                </div>
                                <div class="clearfix"></div>
                                <div class="joined">
                                    <p>2<span>joined</span></p>
                                </div>
                                <div class="clearfix"></div>
                                <div class="comments">
                                    <p>12<span>comments</span></p>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                        <div class="hidden-xs">
                            <div class="expand_dtls">
                                <ul>
                                    <li>
                                        <a href="my-vault.html">
                                            <div class="expand_cnt">
                                                <figure>
                                                    <img src="<%=SkinPath%>images/img_27.png" width="121" height="120" alt="img_27" class="img-responsive">
                                                </figure>
                                                <div class="pos">
                                                    <div class="tbl">
                                                        <div class="tbl_cell">
                                                            <span>3/9</span>
                                                            <small>33%</small>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <h5>SOLVE
                                                <br>
                                                SPACE</h5>
                                        </a>
                                    </li>
                                    <li>
                                        <div class="expand_cnt">
                                            <figure>
                                                <img src="<%=SkinPath%>images/img_28.png" width="121" height="120" alt="img_28" class="img-responsive">
                                            </figure>
                                            <div class="pos">
                                                <div class="tbl">
                                                    <div class="tbl_cell">
                                                        <span>10/16</span>
                                                        <small>63%</small>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <h5>OPEN
                                            <br>
                                            FORUM</h5>
                                    </li>
                                    <li>
                                        <div class="expand_cnt">
                                            <figure>
                                                <img src="<%=SkinPath%>images/img_29.png" width="121" height="120" alt="img_29" class="img-responsive">
                                            </figure>
                                            <div class="pos">
                                                <div class="tbl">
                                                    <div class="tbl_cell">
                                                        <span>4/10</span>
                                                        <small>40%</small>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <h5>COMMUNITY
                                            <br>
                                            CALLS</h5>
                                    </li>
                                    <li>
                                        <div class="expand_cnt">
                                            <figure>
                                                <img src="<%=SkinPath%>images/img_30.png" width="121" height="120" alt="img_30" class="img-responsive">
                                            </figure>
                                            <div class="pos">
                                                <div class="tbl">
                                                    <div class="tbl_cell">
                                                        <span>2/6</span>
                                                        <small>33%</small>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <h5>WEBCAST
                                            <br>
                                            CONVERSATIONS</h5>
                                    </li>
                                    <li>
                                        <div class="expand_cnt">
                                            <figure>
                                                <img src="<%=SkinPath%>images/img_31.png" width="121" height="120" alt="img_31" class="img-responsive">
                                            </figure>
                                            <div class="pos">
                                                <div class="tbl">
                                                    <div class="tbl_cell">
                                                        <span>6/20</span>
                                                        <small>30%</small>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <h5>BLOG
                                            <br>
                                            ARTICLES</h5>
                                    </li>
                                </ul>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                    </div>
                    <div class="open_dtls">
                        <div class="spaces col-sm-6">
                            <div class="spaces_dtls">
                                <div class="space_lt">
                                    <h4>Solve-Spaces</h4>
                                </div>
                                <div class="space_rt">
                                    <a href="my-vault.html">My Vault</a>
                                </div>
                                <div class="clearfix"></div>
                                <div class="solve_dtls">
                                    <ul>
                                        <li>
                                            <div class="solve_lt">
                                                <figure>
                                                    <img src="<%=SkinPath%>images/img_16.png" width="14" height="14" alt="img_16">
                                                </figure>
                                            </div>
                                            <div class="solve_mdl">
                                                <a href="../exercise/intro.html">
                                                    <h5>Market Research Trends</h5>
                                                </a>
                                                <p>Discover what may be causing these trends...</p>
                                                <p>In Progress, Last touch: 10/2/2017</p>
                                            </div>
                                            <div class="solve_rt">
                                                <a href="#">
                                                    <figure>
                                                        <img src="<%=SkinPath%>images/more_icon1.png" width="5" height="15" alt="more_icon1" class="img-responsive">
                                                    </figure>
                                                    <div class="tooltip_7">
                                                        <h5>Market Research Trends</h5>
                                                        <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</p>
                                                    </div>
                                                </a>

                                            </div>
                                            <div class="clearfix"></div>
                                        </li>
                                        <li>
                                            <div class="solve_lt">
                                                <figure>
                                                    <img src="<%=SkinPath%>images/img_17.png" width="14" height="14" alt="img_17">
                                                </figure>
                                            </div>
                                            <div class="solve_mdl">
                                                <a href="../exercise/intro.html">
                                                    <h5>Getting Started With Marketing </h5>
                                                </a>
                                                <p>
                                                    Make your presentations on marketing metrics...
                                                    <br>
                                                    Completed 9/30/2017
                                                </p>
                                            </div>
                                            <div class="solve_rt">
                                                <a href="#">
                                                    <figure>
                                                        <img src="<%=SkinPath%>images/more_icon1.png" width="5" height="15" alt="more_icon1" class="img-responsive">
                                                    </figure>
                                                    <div class="tooltip_8">
                                                        <h5>Getting Started With Marketing</h5>
                                                        <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</p>
                                                    </div>
                                                </a>
                                            </div>
                                            <div class="clearfix"></div>
                                        </li>
                                        <li>
                                            <div class="solve_lt">
                                                <figure>
                                                    <img src="<%=SkinPath%>images/img_17.png" width="14" height="14" alt="img_17">
                                                </figure>
                                            </div>
                                            <div class="solve_mdl">
                                                <a href="../exercise/intro.html">
                                                    <h5>Metrics That Motivate Action 2: Find the Story </h5>
                                                </a>
                                                <p>To help you frame the value proposition of... </p>
                                                <p>Completed 10/1/2017 </p>
                                            </div>
                                            <div class="solve_rt">
                                                <a href="#">
                                                    <figure>
                                                        <img src="<%=SkinPath%>images/more_icon1.png" width="5" height="15" alt="more_icon1" class="img-responsive">
                                                    </figure>
                                                    <div class="tooltip_9">
                                                        <h5>Metrics That Motivate Action 2: Find the Story</h5>
                                                        <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</p>
                                                    </div>
                                                </a>
                                            </div>
                                            <div class="clearfix"></div>
                                        </li>
                                        <li>
                                            <div class="solve_lt">
                                                <figure>
                                                    <img src="<%=SkinPath%>images/img_18.png" width="14" height="14" alt="img_18">
                                                </figure>
                                            </div>
                                            <div class="solve_mdl">
                                                <a href="../exercise/intro.html">
                                                    <h5>Managing Change Before It Manages You</h5>
                                                </a>
                                                <p>Marketers are in a fast-paced environment and... </p>
                                                <p>35 min Recommended </p>
                                            </div>
                                            <div class="solve_rt">
                                                <a href="#">
                                                    <figure>
                                                        <img src="<%=SkinPath%>images/more_icon1.png" width="5" height="15" alt="more_icon1" class="img-responsive">
                                                    </figure>
                                                    <div class="tooltip_10">
                                                        <h5>Metrics That Motivate Action 1: Know Your Audience</h5>
                                                        <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</p>
                                                    </div>
                                                </a>
                                            </div>
                                            <div class="clearfix"></div>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="forum col-sm-6">
                            <div class="connections">
                                <div class="connection_dtls">
                                    <div class="connection_1">
                                        <h4>Open Forum</h4>
                                    </div>
                                    <div class="connection_2">
                                        <a href="#">See all</a>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="connection_cnt">
                                        <ul>
                                            <li>
                                                <div class="connection_lt">
                                                    <figure>
                                                        <img src="<%=SkinPath%>images/img_19.png" width="48" height="48" alt="img_19" class="img-responsive">
                                                    </figure>
                                                </div>
                                                <div class="connection_mdl">
                                                    <h5>Is Talking Endlessly About What Does Your Marketing Organization Look Like - A Visual Assessment</h5>
                                                    <p>
                                                        by Eric Long
                                                        <br>
                                                        Ever wonder what your organization looks like if it were plotted? Here's a sampling...
                                                    </p>
                                                </div>
                                                <div class="connection_rt">
                                                    <a href="#">
                                                        <figure>
                                                            <img src="<%=SkinPath%>images/more_icon1.png" width="5" height="15" alt="more_icon1" class="img-responsive">
                                                        </figure>
                                                        <div class="tooltip_12">
                                                            <h5>Open Forum</h5>
                                                            <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</p>
                                                        </div>
                                                    </a>
                                                </div>
                                                <div class="clearfix"></div>
                                            </li>
                                            <li>
                                                <div class="connection_lt">
                                                    <figure>
                                                        <img src="<%=SkinPath%>images/img_20.png" width="48" height="48" alt="img_20" class="img-responsive">
                                                    </figure>
                                                </div>
                                                <div class="connection_mdl">
                                                    <h5>How Do Marketing Managers Drive Clarity and Consistency Among Their Teams?</h5>
                                                    <p>
                                                        by Mahesh Pari<br>
                                                        Now that it's strategic planning season, I'm thinking about how to do things...
                                                    </p>
                                                </div>
                                                <div class="connection_rt">
                                                    <a href="#">
                                                        <figure>
                                                            <img src="<%=SkinPath%>images/more_icon1.png" width="5" height="15" alt="more_icon1" class="img-responsive">
                                                        </figure>
                                                        <div class="tooltip_13">
                                                            <h5>Open Forum</h5>
                                                            <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</p>
                                                        </div>
                                                    </a>
                                                </div>
                                                <div class="clearfix"></div>
                                            </li>
                                            <li>
                                                <div class="connection_lt">
                                                    <figure>
                                                        <img src="<%=SkinPath%>images/img_21.png" width="48" height="48" alt="img_21" class="img-responsive">
                                                    </figure>
                                                </div>
                                                <div class="connection_mdl">
                                                    <h5>How do Marketing Silos Factor into Your Strategic Planning</h5>
                                                    <p>
                                                        by Saul McGlockton
                                                        <br>
                                                        One of the blogs on the forum points out 10 types of marketing siloea that drive...
                                                    </p>
                                                </div>
                                                <div class="connection_rt">
                                                    <a href="#">
                                                        <figure>
                                                            <img src="<%=SkinPath%>images/more_icon1.png" width="5" height="15" alt="more_icon1" class="img-responsive">
                                                        </figure>
                                                        <div class="tooltip_14">
                                                            <h5>Open Forum</h5>
                                                            <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</p>
                                                        </div>
                                                    </a>
                                                </div>
                                                <div class="clearfix"></div>

                                            </li>

                                        </ul>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="community">
                        <div class="calls col-sm-4">
                            <div class="community_calls">
                                <div class="calls_lt">
                                    <h4>Community Calls</h4>
                                </div>
                                <div class="calls_rt">
                                    <a href="#">My Vault</a>
                                </div>
                                <div class="clearfix"></div>
                                <div class="calls_dtls">
                                    <ul>
                                        <li>
                                            <div class="calls_dtls_lt">
                                                <figure>
                                                    <img src="<%=SkinPath%>images/img_17.png" width="14" height="14" alt="img_17">
                                                </figure>
                                            </div>
                                            <div class="calls_dtls_rt">
                                                <h5>Aligning Goals of Marketing</h5>
                                                <p>Sales and Marketing: partners or foes? Aligned or dysfunctional?</p>
                                            </div>
                                            <div class="clearfix"></div>
                                        </li>
                                        <li>
                                            <div class="calls_dtls_lt">
                                                <figure>
                                                    <img src="<%=SkinPath%>images/img_18.png" width="14" height="14" alt="img_17">
                                                </figure>
                                            </div>
                                            <div class="calls_dtls_rt">
                                                <h5>Balancing Marketing Effectiveness and Follow-Through </h5>
                                                <p>Marketing is a creative field that thrives on flexibility. Yet the Marketing environment is highly demanding, with numerous stakeholders requiring follow-through discipline.</p>
                                            </div>
                                            <div class="clearfix"></div>
                                        </li>
                                        <li>
                                            <div class="calls_dtls_lt">
                                                <figure>
                                                    <img src="<%=SkinPath%>images/img_18.png" width="14" height="14" alt="img_18">
                                                </figure>
                                            </div>
                                            <div class="calls_dtls_rt">
                                                <h5>Increasing Predictability of the Sales Pipeline</h5>
                                                <p>Unpredictability is the new norm.</p>
                                            </div>
                                            <div class="clearfix"></div>
                                        </li>
                                        <li>
                                            <div class="calls_dtls_lt">
                                                <figure>
                                                    <img src="<%=SkinPath%>images/img_18.png" width="14" height="14" alt="img_18">
                                                </figure>
                                            </div>
                                            <div class="calls_dtls_rt">
                                                <h5>Moving from People-Dependency to Process Excellence</h5>
                                                <p>Heroes, martyrs and superstars. We're all familiar with companies that put way too much burden on people because their processes and systems are broken or inadequate.</p>
                                            </div>
                                            <div class="clearfix"></div>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="webcast col-sm-4">
                            <div class="community_calls">
                                <div class="calls_lt">
                                    <h4>Webcast Conversations</h4>
                                </div>
                                <div class="calls_rt">
                                    <a href="#">My Vault</a>
                                </div>
                                <div class="clearfix"></div>
                                <div class="calls_dtls">
                                    <ul>
                                        <li>
                                            <div class="calls_dtls_lt">
                                                <figure>
                                                    <img src="<%=SkinPath%>images/img_18.png" width="14" height="14" alt="img_18">
                                                </figure>
                                            </div>
                                            <div class="calls_dtls_rt">
                                                <h5>How Customer-Centered is Your Group?</h5>
                                                <p>Marketers are always thinking about customers. Even so, it can be easy to take customers for granted or strive to shape or change customers in self-serving ways.</p>
                                            </div>
                                            <div class="clearfix"></div>
                                        </li>
                                        <li>
                                            <div class="calls_dtls_lt">
                                                <figure>
                                                    <img src="<%=SkinPath%>images/img_17.png" width="14" height="14" alt="img_17">
                                                </figure>
                                            </div>
                                            <div class="calls_dtls_rt">
                                                <h5>Catalyzing the Marketing Organization's Learning</h5>
                                                <p>Speed and responsiveness are highly-valued in today's enterprise, but when it comes to marketing organizations transforming into a driving force for enterprise-wide agility and adaptability, learning is the operative word.</p>
                                            </div>
                                            <div class="clearfix"></div>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="blogs col-sm-4">
                            <div class="community_calls">
                                <div class="calls_lt">
                                    <h4>Blogs</h4>
                                    <div class="popup">
                                        <div class="content">
                                            <div class="popup_con1">
                                                <h5>Blogs</h5>
                                                <p>Tap the <span>"Blogs"</span> heading to see all levels.</p>
                                                <p>Tap <span>"My Vault"</span> to see recommended blogs at your proficiency level and above.</p>
                                                <div class="blog_cnt">
                                                    <ul>
                                                        <li>
                                                            <div class="blog_cnt_lt">
                                                                <figure>
                                                                    <img src="<%=SkinPath%>images/img_17.png" width="14" height="14" alt="img_17" class="img-responsive">
                                                                </figure>
                                                            </div>
                                                            <div class="blog_cnt_rt">
                                                                <p>New blog post since you've been here</p>
                                                            </div>
                                                            <div class="clearfix"></div>
                                                        </li>
                                                        <li>
                                                            <div class="blog_cnt_lt">
                                                                <figure>
                                                                    <img src="<%=SkinPath%>images/img_18.png" width="14" height="14" alt="img_18" class="img-responsive">
                                                                </figure>
                                                            </div>
                                                            <div class="blog_cnt_rt">
                                                                <p>No new posts since you've been here</p>
                                                            </div>
                                                            <div class="clearfix"></div>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="calls_rt">
                                    <a href="#">My Vault</a>
                                </div>
                                <div class="clearfix"></div>
                                <div class="calls_dtls">
                                    <ul>
                                        <li>
                                            <div class="calls_dtls_lt">
                                                <figure>
                                                    <img src="<%=SkinPath%>images/img_17.png" width="14" height="14" alt="img_17">
                                                </figure>
                                            </div>
                                            <div class="calls_dtls_rt">
                                                <h5>Customer Experience Transformation Through Proactive Engagement</h5>
                                                <p>"Employee engagement drives transformational changes that enable you to have quality in everything you do," explained Carolyn Muise, Vice President of Total Customer Experience (TCE) at EMC. In our interview on my  Customer Experience Transformation...</p>
                                            </div>
                                            <div class="clearfix"></div>
                                        </li>
                                        <li>
                                            <div class="calls_dtls_lt">
                                                <figure>
                                                    <img src="<%=SkinPath%>images/img_17.png" width="14" height="14" alt="img_17">
                                                </figure>
                                            </div>
                                            <div class="calls_dtls_rt">
                                                <h5>10 Fundamentals To Master Agile Marketing</h5>
                                                <p>Enterprise marketing agility is the adaptability of the organization to readily accommodate spontaneous evolution in the market. The idea of agile marketing is sexy and the potential benefits promising. However, before you put too many eggs in the agile...</p>
                                            </div>
                                            <div class="clearfix"></div>
                                        </li>
                                        <li>
                                            <div class="calls_dtls_lt">
                                                <figure>
                                                    <img src="<%=SkinPath%>images/img_18.png" width="14" height="14" alt="img_18">
                                                </figure>
                                            </div>
                                            <div class="calls_dtls_rt">
                                                <h5>4 Keys To Solving Marketing Silos</h5>
                                                <p>Turf wars, personal agendas, politicking, and "not invented here" syndrome are common internal pains of Marketing silos. It doesn't take a genius to recognize them. Customers see them too! And that's not good. If organizations in your company were ...</p>
                                            </div>
                                            <div class="clearfix"></div>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>
                <div class="cnt_wrapper_rt col-sm-4">
                    <div class="connections">
                        <div class="connection_dtls">
                            <div class="connection_1">
                                <h4>Connections</h4>
                            </div>
                            <div class="connection_2">
                                <a href="#">My Connections</a>
                            </div>
                            <div class="clearfix"></div>
                            <div class="connection_cnt">
                                <ul>
                                    <li>
                                        <div class="connection_lt">
                                            <figure>
                                                <img src="<%=SkinPath%>images/img_3.png" width="49" height="49" alt="img_3" class="img-responsive">
                                            </figure>
                                        </div>
                                        <div class="connection_mdl">
                                            <h5>Sam Eggars</h5>
                                            <p>
                                                CMO, Marketing Tenents Inc...
                                                <br>
                                                Influencer Role: Executive
                                            </p>
                                        </div>
                                        <div class="connection_rt">
                                            <a href="#">
                                                <figure>
                                                    <img src="<%=SkinPath%>images/more_icon1.png" width="5" height="15" alt="more_icon1" class="img-responsive">
                                                </figure>
                                                <div class="tooltip_15">
                                                    <h5>Connections</h5>
                                                    <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</p>
                                                </div>
                                            </a>
                                        </div>
                                        <div class="clearfix"></div>
                                    </li>
                                    <li>
                                        <div class="connection_lt">
                                            <figure>
                                                <img src="<%=SkinPath%>images/img_4.png" width="49" height="49" alt="img_4" class="img-responsive">
                                            </figure>
                                        </div>
                                        <div class="connection_mdl">
                                            <h5>Kourtney Barnes</h5>
                                            <p>
                                                Director of Marketing Initiativ...
                                                <br>
                                                Influencer Role: Executive
                                            </p>
                                        </div>
                                        <div class="connection_rt">
                                            <a href="#">
                                                <figure>
                                                    <img src="<%=SkinPath%>images/more_icon1.png" width="5" height="15" alt="more_icon1" class="img-responsive">
                                                </figure>
                                                <div class="tooltip_16">
                                                    <h5>Connections</h5>
                                                    <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</p>
                                                </div>
                                            </a>
                                        </div>
                                        <div class="clearfix"></div>
                                    </li>
                                    <li>
                                        <div class="connection_lt">
                                            <figure>
                                                <img src="<%=SkinPath%>images/img_5.png" width="49" height="49" alt="img_5" class="img-responsive">
                                            </figure>
                                        </div>
                                        <div class="connection_mdl">
                                            <h5>Diana Lane</h5>
                                            <p>
                                                CEO, Shell Oil Company
                                                <br>
                                                Influencer Role: Executive
                                            </p>
                                        </div>
                                        <div class="connection_rt">
                                            <a href="#">
                                                <figure>
                                                    <img src="<%=SkinPath%>images/more_icon1.png" width="5" height="15" alt="more_icon1" class="img-responsive">
                                                </figure>
                                                <div class="tooltip_17">
                                                    <h5>Connections</h5>
                                                    <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</p>
                                                </div>
                                            </a>
                                        </div>
                                        <div class="clearfix"></div>
                                    </li>
                                    <li>
                                        <div class="connection_lt">
                                            <figure>
                                                <img src="<%=SkinPath%>images/img_6.png" width="49" height="49" alt="img_6" class="img-responsive">
                                            </figure>
                                        </div>
                                        <div class="connection_mdl">
                                            <h5>Jonathon Snowbird-Harrison III</h5>
                                            <p>
                                                Marketing Coordinator, Vision ...
                                                <br>
                                                Influencer Role: Practitio...
                                            </p>
                                        </div>
                                        <div class="connection_rt">
                                            <a href="#">
                                                <figure>
                                                    <img src="<%=SkinPath%>images/more_icon1.png" width="5" height="15" alt="more_icon1" class="img-responsive">
                                                </figure>
                                                <div class="tooltip_18">
                                                    <h5>Connections</h5>
                                                    <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</p>
                                                </div>
                                            </a>
                                        </div>
                                        <div class="clearfix"></div>
                                    </li>
                                </ul>

                            </div>
                        </div>
                    </div>
                    <div class="connections roles">
                        <div class="connection_dtls">
                            <div class="connection_1">
                                <h4 class="mine">Roles Similar to Mine</h4>
                                <div class="popup1">
                                    <div class="content">
                                        <div class="popup_con">
                                            <h5>Roles Similar to mine</h5>
                                            <p>These are people in the entire Value Exchange who share your Influencer Role as an Executive. Other roles are Directors and Practioners. Your "Executive" status was set in the initial questionaire.<a href="#">Learn more. </a></p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="connection_2">
                                <a href="#">See all</a>
                            </div>
                            <div class="clearfix"></div>
                            <div class="connection_cnt">
                                <ul>
                                    <li>
                                        <div class="connection_lt">
                                            <figure>
                                                <img src="<%=SkinPath%>images/img_8.png" width="48" height="48" alt="img_8" class="img-responsive">
                                            </figure>
                                        </div>
                                        <div class="connection_mdl">
                                            <h5>Ryan Ray</h5>
                                            <p>
                                                CMO, FootStomp
                                                <br>
                                                Influencer Role: Executive
                                            </p>
                                        </div>
                                        <div class="connection_rt">
                                            <a href="#">
                                                <figure>
                                                    <img src="<%=SkinPath%>images/more_icon1.png" width="5" height="15" alt="more_icon1" class="img-responsive">
                                                </figure>
                                                <div class="tooltip_19">
                                                    <h5>Roles Similar to Mine</h5>
                                                    <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</p>
                                                </div>
                                            </a>
                                        </div>
                                        <div class="clearfix"></div>
                                    </li>
                                    <li>
                                        <div class="connection_lt">
                                            <figure>
                                                <img src="<%=SkinPath%>images/img_9.png" width="49" height="49" alt="img_9" class="img-responsive">
                                            </figure>
                                        </div>
                                        <div class="connection_mdl">
                                            <h5>Barbara Milton</h5>
                                            <p>
                                                CMO, FluidAngle
                                                <br>
                                                Influencer Role: Executive
                                            </p>
                                        </div>
                                        <div class="connection_rt">
                                            <a href="#">
                                                <figure>
                                                    <img src="<%=SkinPath%>images/more_icon1.png" width="5" height="15" alt="more_icon1" class="img-responsive">
                                                </figure>
                                                <div class="tooltip_20">
                                                    <h5>Roles Similar to Mine</h5>
                                                    <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</p>
                                                </div>
                                            </a>
                                        </div>
                                        <div class="clearfix"></div>
                                    </li>
                                    <li>
                                        <div class="connection_lt">
                                            <figure>
                                                <img src="<%=SkinPath%>images/img_10.png" width="48" height="48" alt="img_10" class="img-responsive">
                                            </figure>
                                        </div>
                                        <div class="connection_mdl">
                                            <h5>Jamie C. Belkin</h5>
                                            <p>
                                                CMO, Amber-Noklin
                                                <br>
                                                Influencer Role: Executive
                                            </p>
                                        </div>
                                        <div class="connection_rt">
                                            <a href="#">
                                                <figure>
                                                    <img src="<%=SkinPath%>images/more_icon1.png" width="5" height="15" alt="more_icon1" class="img-responsive">
                                                </figure>
                                                <div class="tooltip_21">
                                                    <h5>Roles Similar to Mine</h5>
                                                    <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</p>
                                                </div>
                                            </a>
                                        </div>
                                        <div class="clearfix"></div>
                                    </li>
                                    <li>
                                        <div class="connection_lt">
                                            <figure>
                                                <img src="<%=SkinPath%>images/img_11.png" width="48" height="48" alt="img_11" class="img-responsive">
                                            </figure>
                                        </div>
                                        <div class="connection_mdl">
                                            <h5>Mariele LaCroix</h5>
                                            <p>
                                                CEO, CoachesRoom
                                                <br>
                                                Influencer Role: Executive
                                            </p>
                                        </div>
                                        <div class="connection_rt">
                                            <a href="#">
                                                <figure>
                                                    <img src="<%=SkinPath%>images/more_icon1.png" width="5" height="15" alt="more_icon1" class="img-responsive">
                                                </figure>
                                                <div class="tooltip_22">
                                                    <h5>Roles Similar to Mine</h5>
                                                    <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</p>
                                                </div>
                                            </a>
                                        </div>
                                        <div class="clearfix"></div>
                                    </li>
                                </ul>

                            </div>
                        </div>
                    </div>
                    <div class="connections roles">
                        <div class="connection_dtls">
                            <div class="connection_1">
                                <h4>People in My Company</h4>
                            </div>
                            <div class="connection_2">
                                <a href="#">See all</a>
                            </div>
                            <div class="clearfix"></div>
                            <div class="connection_cnt">
                                <ul>
                                    <li>
                                        <div class="connection_lt">
                                            <figure>
                                                <img src="<%=SkinPath%>images/img_12.png" width="48" height="48" alt="img_12" class="img-responsive">
                                            </figure>
                                        </div>
                                        <div class="connection_mdl">
                                            <h5>Tory Hasinawa</h5>
                                            <p>
                                                Marketing Director, Overs-Tops
                                                <br>
                                                Influencer Role: Director
                                            </p>
                                        </div>
                                        <div class="connection_rt">
                                            <a href="#">
                                                <figure>
                                                    <img src="<%=SkinPath%>images/more_icon1.png" width="5" height="15" alt="more_icon1" class="img-responsive">
                                                </figure>
                                                <div class="tooltip_23">
                                                    <h5>People in My Company</h5>
                                                    <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</p>
                                                </div>
                                            </a>
                                        </div>
                                        <div class="clearfix"></div>
                                    </li>
                                    <li>
                                        <div class="connection_lt">
                                            <figure>
                                                <img src="<%=SkinPath%>images/img_13.png" width="48" height="48" alt="img_13" class="img-responsive">
                                            </figure>
                                        </div>
                                        <div class="connection_mdl">
                                            <h5>Alita Bengtsson</h5>
                                            <p>
                                                CMO, Overs-Tops
                                                <br>
                                                Influencer Role: Executive
                                            </p>
                                        </div>
                                        <div class="connection_rt">
                                            <a href="#">
                                                <figure>
                                                    <img src="<%=SkinPath%>images/more_icon1.png" width="5" height="15" alt="more_icon1" class="img-responsive">
                                                </figure>
                                                <div class="tooltip_24">
                                                    <h5>People in My Company</h5>
                                                    <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</p>
                                                </div>
                                            </a>
                                        </div>
                                        <div class="clearfix"></div>
                                    </li>
                                    <li>
                                        <div class="connection_lt">
                                            <figure>
                                                <img src="<%=SkinPath%>images/img_14.png" width="48" height="48" alt="img_14" class="img-responsive">
                                            </figure>
                                        </div>
                                        <div class="connection_mdl">
                                            <h5>John Mays-Tompson</h5>
                                            <p>
                                                Customer Experience, Overs-Tops
                                                <br>
                                                Influencer Role: Practitioner
                                            </p>
                                        </div>
                                        <div class="connection_rt">
                                            <a href="#">
                                                <figure>
                                                    <img src="<%=SkinPath%>images/more_icon1.png" width="5" height="15" alt="more_icon1" class="img-responsive">
                                                </figure>
                                                <div class="tooltip_25">
                                                    <h5>People in My Company</h5>
                                                    <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</p>
                                                </div>
                                            </a>
                                        </div>
                                        <div class="clearfix"></div>
                                    </li>
                                    <li>
                                        <div class="connection_lt">
                                            <figure>
                                                <img src="<%=SkinPath%>images/img_15.png" width="48" height="48" alt="img_15" class="img-responsive">
                                            </figure>
                                        </div>
                                        <div class="connection_mdl">
                                            <h5>Christopher Della Longa</h5>
                                            <p>
                                                Customer Experience, Overs-Tops
                                                <br>
                                                Influencer Role: Practitioner
                                            </p>
                                        </div>
                                        <div class="connection_rt">
                                            <a href="#">
                                                <figure>
                                                    <img src="<%=SkinPath%>images/more_icon1.png" width="5" height="15" alt="more_icon1" class="img-responsive">
                                                </figure>
                                                <div class="tooltip_26">
                                                    <h5>People in My Company</h5>
                                                    <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</p>
                                                </div>
                                            </a>
                                        </div>
                                        <div class="clearfix"></div>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="clearfix"></div>
            </div>
            <footer>
                <div class="footer_dtls">
                    <span>A Service of</span>
                    <a href="<%=DotNetNuke.Common.Globals.NavigateURL(PortalSettings.HomeTabId) %>">
                        <figure>
                            <img src="<%=SkinPath%>images/footer_logo.png" width="129" height="26" alt="footer_logo" class="img-responsive">
                        </figure>
                    </a>
                    <div class="clearfix"></div>
                </div>
                <div class="clearfix"></div>
            </footer>
        </div>
        <div class="clearfix"></div>
    </div>
    <!-- wrapper_cnt ends -->
</div>
