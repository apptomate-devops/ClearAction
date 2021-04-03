<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:utils="af:utils">

    <xsl:import href="label.xsl"/>
    <xsl:import href="attr-common.xsl"/>
    <xsl:import href="attr-container.xsl"/>
    <xsl:output method="html" indent="no" omit-xml-declaration="yes" />

    <xsl:template name="ctl-upload-multi">

        <!--If label is a column, render it here-->
        <xsl:if test="/Form/Settings/LabelAlign != 'inside' and /Form/Settings/LabelAlign != 'top'">
            <xsl:call-template name="ctl-label" />
        </xsl:if>        

        <div>

            <!--If label is top, render it here-->
            <xsl:if test="/Form/Settings/LabelAlign = 'top'">
                <xsl:call-template name="ctl-label" />
            </xsl:if>

            <xsl:if test="ShortDesc != '' and /Form/Settings/LabelAlign = 'inside'">
                <xsl:attribute name="title">
                    <xsl:value-of select="ShortDesc"/>
                </xsl:attribute>
            </xsl:if>

          <div data-settings="settings"
             data-af-apply-layout=""
             data-form="form">
            <xsl:attribute name="data-field">
              <xsl:text>settings.Fields['</xsl:text>
              <xsl:value-of select="Name" />
              <xsl:text>']</xsl:text>
            </xsl:attribute>
            <xsl:attribute name="id">
              <xsl:value-of select="/Form/Settings/BaseId"/>
              <xsl:value-of select="Name" />
            </xsl:attribute>
            <xsl:attribute name="data-ng-model">
              <xsl:text>form.fields.</xsl:text>
              <xsl:value-of select="Name"/>
              <xsl:text>.value</xsl:text>
            </xsl:attribute>
            <div data-af-multi-upload=""
                data-settings="settings"
                data-register-control="registerControl(control)"
                data-register-to-event="registerToEvent(eventName, fn)">
              <xsl:attribute name="data-field">
                <xsl:text>settings.Fields['</xsl:text>
                <xsl:value-of select="Name" />
                <xsl:text>']</xsl:text>
              </xsl:attribute>
                <xsl:attribute name="data-selection">
                    <xsl:text>form.fields.</xsl:text>
                    <xsl:value-of select="Name"/>
                    <xsl:text>.value</xsl:text>
                </xsl:attribute>
            </div>
          </div>

        </div>
    </xsl:template>

</xsl:stylesheet>
