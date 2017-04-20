import numpy as np
import cv2
import window_manager


def main():
    window_name = 'League of Legends'
    window = window_manager.find_window(window_name)

    window_manager.activate_window(window)

    size = window_manager.get_size(window)

    print size


if __name__ == '__main__':
    main()
