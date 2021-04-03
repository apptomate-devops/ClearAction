<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:utils="af:utils">

    <xsl:import href="label.xsl"/>
    <xsl:import href="attr-common.xsl"/>
    <xsl:import href="attr-container.xsl"/>
    <xsl:output method="html" indent="no" omit-xml-declaration="yes" />

    <xsl:template name="ctl-dropdown-autocomplete">
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
            <!--render the control-->
            <div angucomplete-alt=""  input-class="form-control" search-fields="text" title-field="text" match-class="highlight" minlength="1" update-field="updateField(field, val)">
              <xsl:attribute name="local-data">
                <xsl:text>form.fields.</xsl:text>
                <xsl:value-of select="Name"/>
                <xsl:text>.options</xsl:text>
              </xsl:attribute>
              <xsl:attribute name="name">
                <xsl:value-of select="/Form/Settings/BaseId"/>
                <xsl:value-of select="Name"/>
                <xsl:text>-$other</xsl:text>
              </xsl:attribute>
              <xsl:attribute name="data-limit">
                <xsl:value-of select="Limit"/>
              </xsl:attribute>

              <xsl:if test="ShortDesc != '' and /Form/Settings/LabelAlign = 'inside'">
                <xsl:attribute name="title">
                  <xsl:value-of select="ShortDesc"/>
                </xsl:attribute>
              </xsl:if>

              <xsl:call-template name="ctl-attr-common">
                <xsl:with-param name="cssclass">
                  <!--<xsl:text>form-control </xsl:text>-->
                  <xsl:if test="/Form/Settings/ClientSideValidation ='True' and IsRequired='True' and CanValidate = 'True'">required</xsl:if>
                  <xsl:value-of select="$addclass"/>
                </xsl:with-param>
                <xsl:with-param name="hasId">yes</xsl:with-param>
                <xsl:with-param name="hasName">yes</xsl:with-param>
                <xsl:with-param name="bind">yes</xsl:with-param>
              </xsl:call-template>
              <xsl:if test="Mask != ''">
                <xsl:attribute name="data-input-mask">
                  <xsl:value-of select="Mask"/>
                </xsl:attribute>
              </xsl:if>

              <xsl:call-template name="ctl-attr-placeholder" />

              <xsl:attribute name="init-value">
                <xsl:text>'</xsl:text>
                <xsl:value-of select="Value"/>
                <xsl:text>'</xsl:text>
              </xsl:attribute>

              <xsl:attribute name="display-number">
                <xsl:text>'</xsl:text>
                <xsl:value-of select="DisplayNumber"/>
                <xsl:text>'</xsl:text>
              </xsl:attribute>
              <xsl:attribute name="disable-dropdown-on-click">
                <xsl:text>'</xsl:text>
                <xsl:value-of select="DisableDropdownOnClick"/>
                <xsl:text>'</xsl:text>
              </xsl:attribute>
              
              <xsl:if test="IsEnabled != 'True'">
                <xsl:attribute name="disabled">true</xsl:attribute>
              </xsl:if>

              <!--<xsl:attribute name="data-val">
                    <xsl:text>{ text: '{{form.fields.</xsl:text>
                    <xsl:value-of select="Name" />
                    <xsl:text>.selected.path}}', value: 'a' }</xsl:text>
                </xsl:attribute>-->


            </div>
          </div>
        
    </xsl:template>

</xsl:stylesheet>
