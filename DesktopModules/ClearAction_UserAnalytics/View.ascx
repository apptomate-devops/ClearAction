<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="ClearAction.Modules.UserAnalytics.View" %>
<asp:Panel ID="pnlGrayBar" runat="server" >
    <div id="dvAna" style="border-top: 0px;">
        <h4 tooltip-id="tt-analytics">Analytics</h4>
        <div id="tt-analytics" class="popup">
        <div class="content">
            <div class="popup_con">
                <h5>Analytics</h5>
                <p>Your record of completions and next steps. See My Vault links below</p>  
         </div>
        </div>
       </div>
    </div>
   
    <div class="analytics_dtls">
        <div class="sec_title">
            <h5 style="line-height:24px;">MY CONTRIBUTIONS</h5>
        </div>
        <div class="sec_charts">
            <div class="expand">
                <a href="#" class="expand-details">Expand</a>
                <div class="mob_expand hidden visible-xs">
                    <div class="expand_dtls">
                        <ul>
                            <li>
                                <a href="my-vault.html">
                                    <div class="expand_cnt">
                                        <figure>
                                            <div id="solve-space-circle">
                                                <canvas width="180" height="180"></canvas>
                                            </div>
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
                                    <h5><a href="/SolveSpaces">SOLVE
                                        <br>
                                      SPACE<sup>TM</sup></a></h5>
                                </a>
                            </li>
                            <li>
                                <a href="forum.html">
                                    <div class="expand_cnt">
                                        <figure>
                                            <div id="open-forum-circle">
                                                <canvas width="180" height="180"></canvas>
                                            </div>
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
                                    <h5> <a href="/Forum">OPEN
                                        <br>
                                        FORUM</a></h5>
                                </a>
                            </li>
                            <!--
                                    <li>
                                    	<div class="expand_cnt">
                                        	<figure>
                                            	<div id="webcast-conversations-circle"></div>
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
<h5>WEBCAST <br>CONVERSATIONS<sup>TM</sup></h5>
                                    </li>-->
                            <li>
                                <div class="expand_cnt">
                                    <figure>
                                        <div id="insight-articles-circle">
                                            <canvas width="180" height="180"></canvas>
                                        </div>
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
                                <h5> <a href="/Blog">INSIGHTS
                                    <br>
                                   VAULT</a></h5>
                            </li>
                            <li><a href="Community.html">
                                    <div class="expand_cnt">
                                        <figure>
                                            <div id="open-forum-circle">
                                                <canvas width="180" height="180"></canvas>
                                            </div>
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
                                    <h5> <a href="/Forum">Community
                                        <br>
                                        Call</a></h5>
                                </a>
                                    </li>
                        </ul>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>
            <div class="completed CA_Live" style="padding-right:5px;min-width:13%;">
                <p>
                    <label id="lblTopValueS" style="font-weight: normal">0</label><span >done</span><i class="animated-bar" style="width: 100%;"></i>
                </p>
            </div>
            <div class="clearfix CA_Live"></div>
            <div class="interactions" style="padding-right:5px;min-width:13%;">
                <p>
                    <label id="lblTopValueF" style="font-weight: normal">0</label><span>interactions</span><i class="animated-bar" style="width: 100%;"></i>
                </p>
            </div>
            <div class="clearfix CA_Live"></div>
            <div class="comments" style="padding-right:5px;min-width:13%;">
                <p>
                    <label id="lblTopValueI" style="font-weight: normal">0</label><span>comments</span><i class="animated-bar" style="width: 100%;"></i>
                </p>
            </div>
            <div class="clearfix CA_Live"></div>
            <div class="interactions1" style="padding-right:4px;min-width:13%;">
                <p>
                    <label id="lblTopValueCC" style="font-weight: normal">0</label><span>joined</span><i class="animated-bar" style="width: 100%;"></i>
                </p>
            </div>
			<div class="clearfix CA_Live"></div>
            <div class="joined" style="padding-right:0px;min-width:13%;">
                <p>
                    <label id="lblTopValueWC" style="font-weight: normal">0</label><span>joined</span><i class="animated-bar" style="width: 100%;"></i>
                </p>
            </div>
            <div class="clearfix"></div>
        </div>
    </div>
</asp:Panel>

    <div id="dvAnalyticsContainer" class="MainDiv">
    <div id="dvInnerContainer">      
        <asp:Panel ID="pnlSSContainer" CssClass="GraphContainer" runat="server">
            <a href="/SolveSpaces">
                <div id="dvSSContainer">
                    <canvas id="chartSolveSpace" width="120" height="120"></canvas>
                    <div class="GLabelContainer">
                        <label id="lblRatioS" class="CA_innerLabel"></label>
                        <br />
                        <small id="lblPercentS" class="CA_innerSmall"></small>
                    </div>
                    <div class="GraphFooter">
                        <asp:Label ID="lblSSGrap" runat="server" class="GraphLabel">
                        Solve<br />
                          Spaces<sup>TM</sup></asp:Label>
                    </div>
                </div>
            </a>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlForumContainer" CssClass="GraphContainer">
            <a href="/Forum">
                <div id="dvForumContainer">
                    <canvas id="chartForum" width="120" height="120"></canvas>
                    <div class="GLabelContainer">
                        <label id="lblRatioF" class="CA_innerLabel"></label>
                        <br />
                        <small id="lblPercentF" class="CA_innerSmall"></small>
                    </div>
                    <div class="GraphFooter">
                        <asp:Label ID="lblForumGraph" runat="server" class="GraphLabel">
                        Open<br />
                        Forum</asp:Label>
                    </div>
                </div>
            </a>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlInsightContainer" CssClass="GraphContainer">
            <a href="/Blog">
                <div id="dvInsightContainer">
                    <canvas id="chartInsight" width="120" height="120"></canvas>
                    <div class="GLabelContainer">
                        <label id="lblRatioI" class="CA_innerLabel"></label>
                        <br />
                        <small id="lblPercentI" class="CA_innerSmall"></small>
                    </div>

                    <div class="GraphFooter">
                        <asp:Label runat="server" ID="lblInsightLabel" class="GraphLabel">Insights<br />Vault</asp:Label>
                    </div>
                </div>
            </a>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlCCContainer" CssClass="GraphContainer">
            <a href="/Digital/ComponentID/4/IsMyVault/1">
                <div id="dvCCContainer">
                    <canvas id="chartCC" width="120" height="120"></canvas>
                    <div class="GLabelContainer">
                        <label id="lblRatioCC" class="CA_innerLabel"></label>
                        <br />
                        <small id="lblPercentCC" class="CA_innerSmall"></small>
                    </div>
                    <div class="GraphFooter">
                        <asp:Label runat="server" ID="lblCCLabel" class="GraphLabel">Community<br />Calls</asp:Label>
                    </div>
                </div>
            </a>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlWCContainer" CssClass="GraphContainer">
            <a href="/Digital/ComponentID/5/IsMyVault/1">
                <div id="dvWCContainer">
                    <canvas id="chartWC" width="120" height="120"></canvas>
                    <div class="GLabelContainer">
                        <label id="lblRatioWC" class="CA_innerLabel"></label>
                        <br />
                        <small id="lblPercentWC" class="CA_innerSmall"></small>
                    </div>
                    <div class="GraphFooter">
                        <asp:Label runat="server" ID="lblWCLabel" class="GraphLabel">WebCast<br />Conversations<sup>TM</sup></asp:Label>
                    </div>
                </div>
            </a>
        </asp:Panel>
    </div>
</div>
<link href="/DesktopModules/ClearAction_UserAnalytics/css/responsive.css" rel="stylesheet" />
<link href="/DesktopModules/ClearAction_UserAnalytics/module.css?v1=1" rel="stylesheet" />
<style>
    .CA_innerLabel {color:#4d4d4d;font-weight:300;display: inline-block;font-size:20px;font-weight:normal; margin-bottom: 0px;}
    .CA_innerSmall{color:#4d4d4d;font-weight:300;display:inline-block;font-size:14px;font-weight:normal;margin-top:-4px;}

    .GraphLabel{font-weight:500;text-transform:uppercase;color:#684D4D}
    
    .GraphContainerMobile {width: 180px;height: 250px;float: left;margin:auto;margin-right:50px;}
    .GLabelContainer{position: relative; top: -81px;}
    .GraphFooter{position: relative;top: -31px;clear: both;font-size:14px;}

    .MainDiv{width:100%;padding-top:40px;padding-bottom:0px;height:auto;display:table;}

    .CCJoined {display: inline-block;float: left;width: 24%;padding-right: 10px;box-sizing: border-box;}
    .WCJoined {display: inline-block;float: left;width: 24%;padding-right: 10px;box-sizing: border-box;}
</style>
<script type="text/javascript">
    var CA_UserID=<%=this.UserId%>
</script>
<script src="/DesktopModules/ClearAction_SolveSpaces/Scripts/jquery-jtemplates/jquery-jtemplates.js"></script>
<script src="/DesktopModules/ClearAction_SolveSpaces/Scripts/Module/Common.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.1/Chart.bundle.js"></script>
<script src="/DesktopModules/ClearAction_UserAnalytics/Scripts/DougnutGraph/sweet-donut.js"></script>
<script src="/DesktopModules/ClearAction_UserAnalytics/Scripts/Module.js"></script>






