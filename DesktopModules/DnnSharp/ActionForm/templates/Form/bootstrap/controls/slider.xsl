<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:utils="af:utils">

    <xsl:import href="label.xsl"/>
    <xsl:import href="attr-common.xsl"/>
    <xsl:import href="attr-container.xsl"/>
    <xsl:output method="html" indent="no" omit-xml-declaration="yes" />

    <xsl:template name="ctl-slider">
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
            <div load-on-demand="'slider'" >
              <!--<xsl:copy-of select="."/>-->
              <!--render the control-->
              <input ng-cloak="" type="hidden"  data-slider="" data-itemvalue="value" update-field="updateField(field, val)">
                <xsl:attribute name="data-slider-orientation">
                      <xsl:value-of select = "SliderOrientation"></xsl:value-of>
                </xsl:attribute>
                <xsl:attribute name="data-slider-starttext">
                  <xsl:value-of select = "StartText"></xsl:value-of>
                </xsl:attribute>
                <xsl:attribute name="data-slider-endtext">
                  <xsl:value-of select = "EndText"></xsl:value-of>
                </xsl:attribute>
                <xsl:attribute name="data-slider-helptext">
                  <xsl:value-of select = "Help"></xsl:value-of>
                </xsl:attribute>
                <xsl:attribute name="captions">
                  <xsl:value-of select="SliderCaptions"/>
                </xsl:attribute>                
                <xsl:attribute name="caption-classes">
                  <xsl:value-of select="SliderCaptionClasses"/>
                </xsl:attribute>
                <xsl:attribute name="data-slider-min">
                  <xsl:value-of select = "MinValue"></xsl:value-of>
                </xsl:attribute>

                <xsl:attribute name="data-val">
                  <xsl:value-of select="Value"/>
                </xsl:attribute>

                <xsl:attribute name="data-slider-width">
                  <xsl:value-of select="Width"/>
                </xsl:attribute>
                
                <xsl:attribute name="data-slider-max">
                  <xsl:value-of select = "MaxValue"></xsl:value-of>
                </xsl:attribute>

                <xsl:attribute name="data-slider-step">
                  <xsl:value-of select = "StepValue"></xsl:value-of>
                </xsl:attribute>

                <xsl:attribute name="displaytooltip">
                  <xsl:value-of select = "DisplayTooltip"></xsl:value-of>
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
                <xsl:if test="IsEnabled != 'True'">
                  <xsl:attribute name="disabled">disabled</xsl:attribute>
                </xsl:if>
              </input>
            </div>
          </div>
        
    </xsl:template>

</xsl:stylesheet>
