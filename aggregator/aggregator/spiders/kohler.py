"""
Store scraped product information:
local_product_id, source_product_id, sku_id, url, [data we care about], page_hash (for checking diffs), accessed_on

scrapy shell "https://www.us.kohler.com/us/s?Ntt=k"
response.xpath("//a/input[@name='skuId']/@value").getall()
"""

import scrapy


class KohlerSpider(scrapy.Spider):
    name = "kohler"

    def start_requests(self):
        urls = [
            "https://www.us.kohler.com/us/s?Ntt=k"
        ]
        for url in urls:
            yield scrapy.Request(url=url, callback=self.parse)

    def parse(self, response):
        page = response.url.split("/")[-2]
        filename = "kohler-%s.html" % page
        with open(filename, "wb") as f:
            f.write(response.body)
        self.log("Saved file %s" % filename)
