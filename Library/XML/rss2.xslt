<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="2.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:lib="http://www.library/catalog">
  
  <xsl:output method="xml" indent="yes" omit-xml-declaration="yes" />
  
  <xsl:template match="/">
    <rss version="2.0"
         xmlns:lib="http://www.library/catalog">
      <channel>
        <description/>
        <link>http://www.library/catalog</link>
        <title>Documents</title>
        <xsl:apply-templates/>
      </channel>
    </rss>
  </xsl:template>

  <xsl:template match="//lib:Book">
    <lib:Book>
      <xsl:apply-templates mode="copy" select="node()"/>
    </lib:Book>
  </xsl:template>

  <xsl:template match="//lib:Newspaper">
    <lib:Newspaper>
      <xsl:apply-templates mode="copy" select="node()"/>
    </lib:Newspaper>
  </xsl:template>
  
  <xsl:template match="//lib:Patent">
    <lib:Patent>
      <xsl:apply-templates mode="copy" select="node()"/>
    </lib:Patent>
  </xsl:template>

  <xsl:template match="*" mode="copy">
    <xsl:element name="lib:{name()}" namespace="{namespace-uri()}">
      <xsl:apply-templates select="@*|node()" mode="copy" />
    </xsl:element>
  </xsl:template>
  
</xsl:stylesheet>
