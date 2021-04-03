<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:utils="af:utils">

  <xsl:import href="label.xsl"/>
  <xsl:import href="attr-common.xsl"/>
  <xsl:import href="attr-container.xsl"/>
  <xsl:output method="html" indent="no" omit-xml-declaration="yes" />

  <xsl:template name="ctl-recaptcha">
    <xsl:param name="type" />
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
      <script type='text/javascript'>
        onloadCallback = function() {
        $('.g-recaptcha').each(function() {
        $(this).attr('data-widgetid', grecaptcha.render(this, {
        'sitekey': $(this).attr('data-sitekey')
        }));
        });
        };
      </script>
      <script>
        <xsl:attribute name="src">
          <xsl:text>//www.google.com/recaptcha/api.js?onload=onloadCallback&amp;render=explicit&amp;hl=</xsl:text>
          <xsl:value-of select="/Form/Settings/CultureCode"/>
        </xsl:attribute>
      </script>

      <div>
        <xsl:call-template name="ctl-attr-common">
          <xsl:with-param name="cssclass">
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
        <label for="g-recaptcha-response" style="display:none;">CAPTCHA</label>
        <div class="g-recaptcha">
          <xsl:attribute name="data-sitekey">
            <xsl:value-of select="Data/SiteKey"/>
          </xsl:attribute>
          <xsl:attribute name="id">
            <xsl:value-of select="/Form/Settings/BaseId"/>
            <xsl:text>-</xsl:text>
            <xsl:value-of select="Id"/>
            <xsl:text>-recaptcha</xsl:text>
          </xsl:attribute>
        </div>
      </div>
    </div>
  </xsl:template>

</xsl:stylesheet>
