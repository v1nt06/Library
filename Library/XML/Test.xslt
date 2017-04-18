<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
                xmlns="http://www.library/catalog"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:lib="http://www.library/catalog">

<xsl:output method="xml" indent="yes" />

  <xsl:template match="/lib:CatalogContent">
    <CatalogContent>
      <xsl:apply-templates/>
    </CatalogContent>
  </xsl:template>

  <xsl:template match="//lib:Documents">
    <Documents>
      <xsl:apply-templates/>
    </Documents>
  </xsl:template>

  <xsl:template match="//lib:Document[@type=&quot;Book&quot;]">
    <Book>
      <xsl:apply-templates mode="copy" select="node()"/>
    </Book>
  </xsl:template>

  <xsl:template match="//lib:Document[@type=&quot;Newspaper&quot;]">
    <Newspaper>
      <xsl:apply-templates mode="copy" select="node()"/>
    </Newspaper>
  </xsl:template>

  <xsl:template match="//lib:Document[@type=&quot;Patent&quot;]">
    <Patent>
      <xsl:apply-templates mode="copy" select="node()"/>
    </Patent>
  </xsl:template>

  <xsl:template match="*" mode="copy">
    <xsl:element name="{name()}" namespace="{namespace-uri()}">
      <xsl:apply-templates select="@*|node()" mode="copy" />
    </xsl:element>
  </xsl:template>

</xsl:stylesheet>
