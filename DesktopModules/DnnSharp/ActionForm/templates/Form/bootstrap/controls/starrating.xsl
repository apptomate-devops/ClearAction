<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:utils="af:utils">

  <xsl:import href="attr-common.xsl"/>
  <xsl:import href="attr-container.xsl"/>
  <xsl:import href="label.xsl"/>
  <xsl:output method="html" indent="no" omit-xml-declaration="yes" />

  <xsl:template name="ctl-starrating">
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
      <div>
        <input star-rating="">
          <xsl:call-template name="ctl-attr-common">
            <xsl:with-param name="cssclass">
              <xsl:text>form-control </xsl:text>
              <xsl:if test="/Form/Settings/ClientSideValidation ='True' and IsRequired='True' and CanValidate = 'True'">
                required
              </xsl:if>
              <xsl:text> </xsl:text>
              <xsl:value-of select="$addclass"/>
            </xsl:with-param>
            <xsl:with-param name="hasId">yes</xsl:with-param>
            <xsl:with-param name="hasName">yes</xsl:with-param>
            <xsl:with-param name="bind">yes</xsl:with-param>
            <xsl:with-param name="touchEvent">keyup</xsl:with-param>
          </xsl:call-template>
          <xsl:attribute name="on-stars-change">
            <xsl:text>uptStarRating(value,name)</xsl:text>
          </xsl:attribute>
          <xsl:attribute name="field-id">
            <xsl:value-of select="Id"/>
          </xsl:attribute>
          <xsl:attribute name="fname">
            <xsl:text>'</xsl:text>
            <xsl:value-of select="Name"/>
            <xsl:text>'</xsl:text>
          </xsl:attribute>
          <xsl:attribute name="starsno">
            <xsl:value-of select="StarsNo"/>
          </xsl:attribute>
          <xsl:attribute name="step">
            <xsl:value-of select="Step"/>
          </xsl:attribute>
          <xsl:attribute name="size">
            <xsl:text>'</xsl:text>
            <xsl:value-of select="Size"/>
            <xsl:text>'</xsl:text>
          </xsl:attribute>
          <xsl:attribute name="minimumrating">
            <xsl:value-of select="MinimumRating"/>
          </xsl:attribute>
          <xsl:attribute name="maximumrating">
            <xsl:value-of select="MaximumRating"/>
          </xsl:attribute>
          <xsl:attribute name="selectedstarcolor">
            <xsl:text>'</xsl:text>
            <xsl:value-of select="SelectedStarColor"/>
            <xsl:text>'</xsl:text>
          </xsl:attribute>
          <xsl:attribute name="unselectedstarcolor">
            <xsl:text>'</xsl:text>
            <xsl:value-of select="UnselectedStarColor"/>
            <xsl:text>'</xsl:text>
          </xsl:attribute>
          <xsl:attribute name="starcaptions">
            <xsl:value-of select="StarCaptions"/>
          </xsl:attribute>
          <xsl:attribute name="starcaptionclasses">
            <xsl:value-of select="StarCaptionClasses"/>
          </xsl:attribute>
          <xsl:attribute name="showcaption">
            <xsl:text>'</xsl:text>
            <xsl:value-of select="ShowCaption"/>
            <xsl:text>'</xsl:text>
          </xsl:attribute>
          <xsl:attribute name="showclear">
            <xsl:text>'</xsl:text>
            <xsl:value-of select="ShowClear"/>
            <xsl:text>'</xsl:text>
          </xsl:attribute>
          <xsl:attribute name="value">
            <xsl:value-of select="Value"/>
          </xsl:attribute>
          <xsl:attribute name="isenabled">
            <xsl:text>'</xsl:text>
            <xsl:value-of select="IsEnabled"/>
            <xsl:text>'</xsl:text>
          </xsl:attribute>
        </input>
        </div>
      </div>

  </xsl:template>

</xsl:stylesheet>
