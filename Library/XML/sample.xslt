<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:lib="http://www.library/catalog">

  <xsl:output method="xml" indent="yes" omit-xml-declaration="no"/>

  <xsl:template match="/">
    <catalog>
      <xsl:apply-templates/>
    </catalog>
  </xsl:template>

  <xsl:template match="//lib:Book">
    <book>
      <xsl:apply-templates mode="copy" select="node()"/>
    </book>
  </xsl:template>

  <xsl:template match="//lib:Newspaper">
    <newspaper>
      <xsl:apply-templates mode="copy" select="node()"/>
    </newspaper>
  </xsl:template>

  <xsl:template match="//lib:Patent">
    <patent>
      <xsl:apply-templates mode="copy" select="node()"/>
    </patent>
  </xsl:template>

  <xsl:template match="*" mode="copy">
    <xsl:element name="{translate(name(.),
                 'ABCDEFGHIJKLMNOPQRSTUVWXYZ',
                 'abcdefghijklmnopqrstuvwxyz')}">
      <xsl:apply-templates select="@*|node()" mode="copy" />
    </xsl:element>
  </xsl:template>

  <xsl:template mode="copy" match="//lib:PagesCount">
    <pages_count>
      <xsl:value-of select="node()"/>
    </pages_count>
  </xsl:template>

  <xsl:template mode="copy" match="//lib:PublicationDate">
    <publication_date>
      <xsl:value-of select="node()"/>
    </publication_date>
  </xsl:template>

  <xsl:template mode="copy" match="//lib:PlaceOfPublication">
    <place_of_publication>
      <xsl:value-of select="node()"/>
    </place_of_publication>
  </xsl:template>

  <xsl:template mode="copy" match="//lib:FirstName">
    <first_name>
      <xsl:value-of select="node()"/>
    </first_name>
  </xsl:template>

  <xsl:template mode="copy" match="//lib:LastName">
    <last_name>
      <xsl:value-of select="node()"/>
    </last_name>
  </xsl:template>

  <xsl:template mode="copy" match="//lib:ApplicationDate">
    <application_date>
      <xsl:value-of select="node()"/>
    </application_date>
  </xsl:template>

  <xsl:template mode="copy" match="//lib:RegistrationNumber">
    <registration_number>
      <xsl:value-of select="node()"/>
    </registration_number>
  </xsl:template>
  
</xsl:stylesheet>
