﻿<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:lib="http://www.library/catalog"
                xmlns:msxsl="urn:schemas-microsoft-com:xslt"
                extension-element-prefixes="msxsl">

  <xsl:output method="xml" indent="yes" omit-xml-declaration="yes"/>

  <msxsl:script language="JScript" implements-prefix="lib">
    function today()
    {
      return (new Date()).toString();
    }
  </msxsl:script>

  <xsl:template match="//lib:Documents">
    <xsl:text disable-output-escaping="yes">&lt;!DOCTYPE html&gt;</xsl:text>
    <html>
      <xsl:call-template name="head"/>
      <body>
        <h1>Current funds</h1>
        <h2>
          Today: <xsl:value-of select="lib:today()"/>
        </h2>
        <table border="1">
          <caption>Books</caption>
          <tr>
            <th>Name</th>
            <th>Pages count</th>
            <th>Annotation</th>
            <th>Publication date</th>
            <th>ISBN</th>
          </tr>
          <xsl:apply-templates select="//lib:Book"/>
          <tr>
            <td>Total:</td>
            <td colspan="4">
              <xsl:value-of select="count(//lib:Book)"/>
            </td>
          </tr>
        </table>
        <br/>
        <table border="1">
          <caption>Newspapers</caption>
          <tr>
            <th>Name</th>
            <th>Pages count</th>
            <th>Annotation</th>
            <th>Publication date</th>
            <th>Number</th>
            <th>ISSN</th>
          </tr>
          <xsl:apply-templates select="//lib:Newspaper"/>
          <tr>
            <td>Total:</td>
            <td colspan="5">
              <xsl:value-of select="count(//lib:Newspaper)"/>
            </td>
          </tr>
        </table>
        <br/>
        <table border="1">
          <caption>Patents</caption>
          <tr>
            <th>Name</th>
            <th>Pages count</th>
            <th>Annotation</th>
            <th>Publication date</th>
            <th>Country</th>
            <th>Application date</th>
            <th>Registration number</th>
          </tr>
          <xsl:apply-templates select="//lib:Patent"/>
          <tr>
            <td>Total:</td>
            <td colspan="6">
              <xsl:value-of select="count(//lib:Patent)"/>
            </td>
          </tr>
        </table>
        <h2>
          Documents count: <xsl:value-of select="count(//lib:Documents/*)"/>
        </h2>
        <h1>Authors</h1>
        <h2>
          Today: <xsl:value-of select="lib:today()"/>
        </h2>
        <table border="1">
          <caption>Authors</caption>
          <tr>
            <th>First name</th>
            <th>Last name</th>
            <th>Works</th>
            <th>Works count</th>
          </tr>
          <xsl:for-each select="//lib:Person">
            <xsl:call-template name="AuthorsList">
              <xsl:with-param name="firstName" select="node()[2]"/>
              <xsl:with-param name="lastName" select="node()[4]"/>
            </xsl:call-template>
          </xsl:for-each>
          <tr>
            <td>
              <b>Total authors:</b>
            </td>
            <td>
              <xsl:value-of select="count(//lib:Person[not(. = preceding::lib:Person)])"/>
            </td>
            <td>
              <b>Total works:</b>
            </td>
            <td>
              <xsl:value-of select="count(//lib:Documents/*[descendant::lib:Person])"/>
            </td>
          </tr>
        </table>
      </body>
    </html>
  </xsl:template>

  <xsl:key name="fullName" match="//lib:Person" use="node()[2]/text() + node()[4]/text()"/>

  <xsl:template name="AuthorsList">
    <xsl:param name="firstName"/>
    <xsl:param name="lastName"/>
    <xsl:variable name="count" select="count(node()/following::lib:Person[lib:FirstName[text() = $firstName] and lib:LastName[text() = $lastName]])"/>
    <xsl:if test="$count = 0">
      <xsl:call-template name="AuthorsTable">
        <xsl:with-param name="firstName" select="$firstName"/>
        <xsl:with-param name="lastName" select="$lastName"/>
      </xsl:call-template>
    </xsl:if>
  </xsl:template>
  
  <xsl:template name="AuthorsTable">
    <xsl:param name="firstName"/>
    <xsl:param name="lastName"/>
    <tr>
      <td>
        <xsl:value-of select="$firstName"/>
      </td>
      <td>
        <xsl:value-of select="$lastName"/>
      </td>
      <td>
        <xsl:for-each select="//lib:Documents/*[descendant::lib:Person[lib:FirstName[text() = $firstName] and lib:LastName[text() = $lastName]]]/lib:Name">
          <xsl:value-of select="node()"/><br/>
        </xsl:for-each>
      </td>
      <td>
        <xsl:value-of select="count(//lib:Person[lib:FirstName[text() = $firstName] and lib:LastName[text() = $lastName]])"/>
      </td>
    </tr>
  </xsl:template>

  <xsl:template name="head">
    <head>
      <title>Catalog Reports</title>
      <meta charset="utf-8"/>
    </head>
  </xsl:template>

  <xsl:template match="//lib:Book">
    <tr>
      <td>
        <xsl:value-of select="*[name()='Name']"/>
      </td>
      <td>
        <xsl:value-of select="*[name()='PagesCount']"/>
      </td>
      <td>
        <xsl:value-of select="*[name()='Annotation']"/>
      </td>
      <td>
        <xsl:value-of select="*[name()='PublicationDate']"/>
      </td>
      <td>
        <xsl:value-of select="*[name()='ISBN']"/>
      </td>
    </tr>
  </xsl:template>

  <xsl:template match="//lib:Newspaper">
    <tr>
      <td>
        <xsl:value-of select="*[name()='Name']"/>
      </td>
      <td>
        <xsl:value-of select="*[name()='PagesCount']"/>
      </td>
      <td>
        <xsl:value-of select="*[name()='Annotation']"/>
      </td>
      <td>
        <xsl:value-of select="*[name()='PublicationDate']"/>
      </td>
      <td>
        <xsl:value-of select="*[name()='Number']"/>
      </td>
      <td>
        <xsl:value-of select="*[name()='ISSN']"/>
      </td>
    </tr>
  </xsl:template>

  <xsl:template match="//lib:Patent">
    <tr>
      <td>
        <xsl:value-of select="*[name()='Name']"/>
      </td>
      <td>
        <xsl:value-of select="*[name()='PagesCount']"/>
      </td>
      <td>
        <xsl:value-of select="*[name()='Annotation']"/>
      </td>
      <td>
        <xsl:value-of select="*[name()='PublicationDate']"/>
      </td>
      <td>
        <xsl:value-of select="*[name()='Country']"/>
      </td>
      <td>
        <xsl:value-of select="*[name()='ApplicationDate']"/>
      </td>
      <td>
        <xsl:value-of select="*[name()='RegistrationNumber']"/>
      </td>
    </tr>
  </xsl:template>

</xsl:stylesheet>
