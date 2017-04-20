import urllib2
import urlparse
import os
import json


def urljoin(*args):
    return '/'.join(map(lambda x: str(x).rstrip('/'), args))


class DataDragon:
    language = 'en_US'
    cdn_url = ''
    mastery_version = ''

    def __init__(self, base_url, base_dir):
        self.base_url = base_url
        self.base_dir = base_dir

    def __build_url(self, url):
        return urlparse.urljoin(self.base_url, url)

    def __build_cdn_url(self, url):
        return urljoin(self.cdn_url, url)

    def __save_json(self, filename, json_data):
        filepath = os.path.join(self.base_dir, filename)
        dirname = os.path.dirname(filepath)
        if not os.path.exists(dirname):
            os.makedirs(dirname)
        with open(filepath, 'w') as outfile:
            json.dump(json.loads(json_data), outfile)

    def __load_json(self, filename):
        filepath = os.path.join(self.base_dir, filename)
        with open(filepath, 'r') as infile:
            return json.load(infile)

    def initialize(self, realm):
        self.download_realm_data(realm)

        realm_data = self.__load_json('realms/{}.json'.format(realm))
        self.cdn_url = realm_data['cdn']
        self.mastery_version = realm_data['n']['mastery']

        self.download_masteries()
        self.download_mastery_images()

    def download_realm_data(self, realm):
        url = self.__build_url('/realms/{realm}.json'.format(realm=realm))
        response = urllib2.urlopen(url)
        self.__save_json('realms/{}.json'.format(realm), response.read())

    def download_masteries(self):
        url = self.__build_cdn_url('{version}/data/{language}/mastery.json'.format(
            version=self.mastery_version, language=self.language))
        print url
        response = urllib2.urlopen(url)
        self.__save_json('masteries.json', response.read())

    def download_mastery_images(self):
        masteries_data = self.__load_json('masteries.json')

        mastery_ids = []
        for tree, masteries in masteries_data['tree'].items():
            for row in masteries:
                mastery_ids.extend([mastery['masteryId'] for mastery in row])
            # print masteries
            # print [mastery['id'] for mastery in masteries]
        for mastery_id in mastery_ids:
            self.download_mastery_image(mastery_id)

    def download_mastery_image(self, mastery_id):
        url = self.__build_cdn_url('{version}/img/mastery/{mastery_id}.png'.format(version=self.mastery_version, mastery_id=mastery_id))
        response = urllib2.urlopen(url)
        directory = os.path.join(self.base_dir, 'images/masteries')
        if not os.path.exists(directory):
            os.makedirs(directory)
        filename = os.path.join(directory, '{}.png'.format(mastery_id))
        with open(filename, 'wb') as outfile:
            outfile.write(response.read())
            outfile.close()
