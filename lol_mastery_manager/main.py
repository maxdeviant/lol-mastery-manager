import os
import numpy as np
import cv2
import window_manager
from data_dragon import DataDragon

DATA_DRAGON_URL = 'https://ddragon.leagueoflegends.com'
DATA_DIRECTORY = os.path.join(os.path.dirname(
    os.path.realpath(__file__)), '../data')


def main():
    realm = 'na'

    data_dragon = DataDragon(DATA_DRAGON_URL, DATA_DIRECTORY)
    data_dragon.download_realm_data(realm)
    data_dragon.initialize(realm)
    # data_dragon.download_mastery_image()

    # window_name = 'League of Legends'
    # window = window_manager.find_window(window_name)

    # window_manager.activate_window(window)

    # size = window_manager.get_size(window)

    # print size


if __name__ == '__main__':
    main()
