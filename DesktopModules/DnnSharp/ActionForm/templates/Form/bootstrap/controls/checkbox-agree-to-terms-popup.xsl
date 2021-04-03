<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:utils="af:utils">

    <xsl:import href="label.xsl"/>
    <xsl:import href="attr-common.xsl"/>
    <xsl:import href="attr-container.xsl"/>
    <xsl:output method="html" indent="no" omit-xml-declaration="yes" />

    <xsl:template name="ctl-checkbox-agree-to-terms-popup">

        <div>
            <xsl:call-template name="ctl-attr-container">
                <xsl:with-param name="addClasses">
                    <xsl:text> form-checkbox-agree-to-terms-popup </xsl:text>
                    <xsl:if test="ColOffset > 0">
                        <xsl:text> col-sm-offset-</xsl:text>
                        <xsl:value-of select="ColOffset"/>
                    </xsl:if>
                </xsl:with-param>
                <xsl:with-param name="withLabelColumn">false</xsl:with-param>
            </xsl:call-template>

            <div class="checkbox">
                <label>
                    <input type="checkbox">
                        <xsl:call-template name="ctl-attr-common">
                            <xsl:with-param name="cssclass">
                                <xsl:text> normalCheckBox </xsl:text>
                                <xsl:if test="/Form/Settings/ClientSideValidation ='True' and IsRequired='True' and CanValidate = 'True'"> required </xsl:if>
                            </xsl:with-param>
                            <xsl:with-param name="hasId">yes</xsl:with-param>
                            <xsl:with-param name="hasName">yes</xsl:with-param>
                            <xsl:with-param name="bind">yes</xsl:with-param>
                            <xsl:with-param name="touchEvent">click</xsl:with-param>
                        </xsl:call-template>
                        <xsl:if test="Value = 'True'">
                            <xsl:attribute name="checked">checked</xsl:attribute>
                        </xsl:if>
                        <xsl:if test="IsEnabled != 'True'">
                            <xsl:attribute name="disabled">disabled</xsl:attribute>
                        </xsl:if>
                    </input>

                    <xsl:value-of select="utils:formatString(utils:localize('agreeToTermsPopup1.text', 'Agree to '), utils:tokenize(Url))" disable-output-escaping="yes"></xsl:value-of>
                    
                    <span class="" data-toggle="modal-bs3">
                        <xsl:attribute name="data-target">
                            <xsl:text>#AgreeToTermsModal</xsl:text>
                            <xsl:value-of select="Name"/>
                        </xsl:attribute>
                        <xsl:value-of select="utils:formatString(utils:localize('agreeToTermsPopup2.text', '&lt;a href=&quot;{0}&quot;&gt;Terms and Conditions&lt;/a&gt;'), utils:tokenize(Url))" disable-output-escaping="yes"></xsl:value-of>
                    </span>
                </label>
                <div class="err-placeholder"></div>

                <!-- modal stuff-->
                <div class="modal-bs3 modal fade">
                    <xsl:attribute name="id">
                        <xsl:text>AgreeToTermsModal</xsl:text>
                        <xsl:value-of select="Name"/>
                    </xsl:attribute>
                    
                    <div class="modal-dialog">

                        <div class="modal-content">
                            <div class="modal-header">
                              <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                              <h4 class="modal-title">
                                <xsl:value-of select="utils:formatString(utils:localize('agreeToTermsPopupTitle.text', 'Terms and Conditions '), utils:tokenize(Url))" disable-output-escaping="yes"></xsl:value-of>
                              </h4>
                            </div>
                            <div class="modal-body">
                                <p>
                                    <xsl:value-of select="Text" disable-output-escaping="yes"/>
                                </p>
                            </div>
                            <div class="modal-footer">
                              <button type="button" class="btn btn-default" data-dismiss="modal">
                                <xsl:value-of select="utils:formatString(utils:localize('agreeToTermsPopupCloseButton.text', 'Close '), utils:tokenize(Url))" disable-output-escaping="yes"></xsl:value-of>
                              </button>
                            </div>
                            <!-- /.modal-content -->
                        </div>
                        <!-- /.modal-dialog -->
                    </div>
                    <!-- /.modal -->
                </div>

            </div>
        </div>


    </xsl:template>

</xsl:stylesheet>
