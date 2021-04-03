<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:utils="af:utils">

  <xsl:import href="label.xsl"/>
  <xsl:import href="attr-common.xsl"/>
  <xsl:import href="attr-container.xsl"/>
  <xsl:output method="html" indent="no" omit-xml-declaration="yes" />

  <xsl:template name="ctl-trumbowyg">
    <xsl:param name="addclass" />

    <!--If label is a column, render it here-->
    <xsl:if test="/Form/Settings/LabelAlign != 'inside' and /Form/Settings/LabelAlign != 'top'">
      <xsl:call-template name="ctl-label" />
    </xsl:if>

    <div>
      <xsl:call-template name="ctl-attr-container" />

      <!--If label is top, render it here-->
      <xsl:if test="/Form/Settings/LabelAlign = 'top'">
        <xsl:call-template name="ctl-label" />
      </xsl:if>
      <div load-on-demand="'trumbowyg'">
        <!--render the control-->
        <div trumbowyg="" data-itemvalue="value" update-field="updateField(field, val)">

          <xsl:attribute name="initial-value">
            <xsl:value-of select="InitalValue"/>
          </xsl:attribute>

          <xsl:attribute name="btns">
            <xsl:value-of select="Buttons"/>
          </xsl:attribute>

          <xsl:attribute name="btns-def">
            <xsl:value-of select="ButtonDropdowns"/>
          </xsl:attribute>

          <xsl:attribute name="btns-grps">
            <xsl:value-of select="BtnGroups"/>
          </xsl:attribute>
          
          <xsl:attribute name="ngdisabled">
            <xsl:value-of select="IsEnabled"/>
          </xsl:attribute>

          <xsl:attribute name="lang">
            <xsl:value-of select="Language"/>
          </xsl:attribute>

          <xsl:attribute name="theme">
            <xsl:value-of select="DarkTheme"/>
          </xsl:attribute>
          
          <xsl:if test="ShortDesc != '' and /Form/Settings/LabelAlign = 'inside'">
            <xsl:attribute name="title">
              <xsl:value-of select="ShortDesc"/>
            </xsl:attribute>
          </xsl:if>

          <xsl:call-template name="ctl-attr-common">
            <xsl:with-param name="cssclass">
              <xsl:value-of select="$addclass"/>
            </xsl:with-param>
            <xsl:with-param name="hasId">yes</xsl:with-param>
            <xsl:with-param name="hasName">yes</xsl:with-param>
            <xsl:with-param name="bind">yes</xsl:with-param>
          </xsl:call-template>
          <xsl:call-template name="ctl-attr-placeholder" />

        </div>
      </div>
    </div>

  </xsl:template>

</xsl:stylesheet>
