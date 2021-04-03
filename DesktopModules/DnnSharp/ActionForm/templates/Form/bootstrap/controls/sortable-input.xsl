<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:utils="af:utils">

    <xsl:import href="label.xsl"/>
    <xsl:import href="attr-common.xsl"/>
    <xsl:import href="attr-container.xsl"/>
    <xsl:output method="html" indent="no" omit-xml-declaration="yes" />

    <xsl:template name="ctl-sortable">
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
            <div load-on-demand="'sortable'">
              <!--render the control-->
              <div  data-sortable="" data-itemvalue="value" update-field="updateField(field, val)">

                <xsl:attribute name="data-items">
                  <xsl:text>form.fields.</xsl:text>
                  <xsl:value-of select="Name"/>
                  <xsl:text>.options</xsl:text>
                </xsl:attribute>
                <xsl:attribute name="data-icon-class">
                  <xsl:value-of select="CssIconClass"/>
                </xsl:attribute>
                <xsl:attribute name="data-icon-position">
                  <xsl:value-of select="IconPosition"/>
                </xsl:attribute>
                <xsl:attribute name="data-drag-by-icon">
                  <xsl:value-of select="DragByIcon"/>
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

              </div>
            </div>
          </div>
        
    </xsl:template>

</xsl:stylesheet>
