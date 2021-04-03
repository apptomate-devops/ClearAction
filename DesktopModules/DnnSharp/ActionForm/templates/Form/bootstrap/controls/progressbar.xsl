<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:utils="af:utils">

  <xsl:import href="attr-common.xsl"/>
  <xsl:import href="attr-container.xsl"/>
  <xsl:import href="label.xsl"/>
  <xsl:output method="html" indent="no" omit-xml-declaration="yes" />

  <xsl:template name="ctl-progressbar">
    <xsl:param name="addclass" />
    <!--<xsl:copy-of select="."/>-->
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
      <div load-on-demand="'progressbar'">
        <div progressbar="">
          <xsl:call-template name="ctl-attr-common">
            <xsl:with-param name="cssclass">
              <xsl:text>prgrs model-only </xsl:text>
              <xsl:if test="/Form/Settings/ClientSideValidation ='True' and IsRequired='True' and CanValidate = 'True'">
                required
              </xsl:if>
              <xsl:text> </xsl:text>
              <xsl:value-of select="$addclass"/>
            </xsl:with-param>
            <xsl:with-param name="hasId">yes</xsl:with-param>
            <xsl:with-param name="hasName">yes</xsl:with-param>
            <xsl:with-param name="bind">no</xsl:with-param>
            <xsl:with-param name="touchEvent">keyup</xsl:with-param>
          </xsl:call-template>
          <xsl:attribute name="value">
            <xsl:text>form.fields.</xsl:text>
            <xsl:value-of select="Name"/>
            <xsl:text>.value</xsl:text>
          </xsl:attribute>
          <xsl:attribute name="ptype">
            <xsl:text>'</xsl:text>
            <xsl:value-of select="PType"/>
            <xsl:text>'</xsl:text>
          </xsl:attribute>
          <xsl:attribute name="displaytexttype">
            <xsl:text>'</xsl:text>
            <xsl:value-of select="DisplayTextType"/>
            <xsl:text>'</xsl:text>
          </xsl:attribute>
          <xsl:attribute name="field-id">
            <xsl:value-of select="Id"/>
          </xsl:attribute>
          <xsl:attribute name="fname">
            <xsl:text>'</xsl:text>
            <xsl:value-of select="Name"/>
            <xsl:text>'</xsl:text>
          </xsl:attribute>
          <xsl:attribute name="minval">
            <xsl:value-of select="MinVal"/>
          </xsl:attribute>
          <xsl:attribute name="maxval">
            <xsl:value-of select="MaxVal"/>
          </xsl:attribute>
          <xsl:attribute name="strokecolor">
            <xsl:text>'</xsl:text>
            <xsl:value-of select="StrokeColor"/>
            <xsl:text>'</xsl:text>
          </xsl:attribute>
          <xsl:attribute name="trailcolor">
            <xsl:text>'</xsl:text>
            <xsl:value-of select="TrailColor"/>
            <xsl:text>'</xsl:text>
          </xsl:attribute>
          <xsl:attribute name="strokewidth">
            <xsl:text>'</xsl:text>
            <xsl:value-of select="StrokeWidth"/>
            <xsl:text>'</xsl:text>
          </xsl:attribute>
          <xsl:attribute name="trailwidth">
            <xsl:text>'</xsl:text>
            <xsl:value-of select="TrailWidth"/>
            <xsl:text>'</xsl:text>
          </xsl:attribute>
          <xsl:attribute name="animation">
            <xsl:text>'</xsl:text>
            <xsl:value-of select="Animation"/>
            <xsl:text>'</xsl:text>
          </xsl:attribute>
          <xsl:attribute name="animationduration">
            <xsl:text>'</xsl:text>
            <xsl:value-of select="AnimationDuration"/>
            <xsl:text>'</xsl:text>
          </xsl:attribute>
          <xsl:attribute name="valuecolor">
            <xsl:text>'</xsl:text>
            <xsl:value-of select="ValueColor"/>
            <xsl:text>'</xsl:text>
          </xsl:attribute>
        </div>
        
      </div>
    </div>

    <style>
      .prgrs{
        margin: 10px 20px 20px 20px;
        position: relative;
      }
    </style>

    

  </xsl:template>

</xsl:stylesheet>
