<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="2.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:lib="http://www.library/catalog">

  <xsl:output method="xml" indent="yes" omit-xml-declaration="yes" />

  <xsl:template match="/">
    <feed xmlns="http://www.w3.org/2005/Atom"
          xmlns:lib="http://www.library/catalog">
      <title/>
      <id>urn:uuid:60a76c80-d399-11d9-b91C-0003939e0af6</id>
      <updated>2003-12-13T18:30:02Z</updated>
      <xsl:apply-templates/>
    </feed>
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
