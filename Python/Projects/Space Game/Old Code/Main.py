# /usr/bin/env python
"""
This simple example is used for the line-by-line tutorial
that comes with pygame. It is based on a 'popular' web banner.
Note there are comments here, but for the full explanation,
follow along in the tutorial.
"""

# Import Modules
import os
import pygame
from pygame.locals import *

if not pygame.font: print('Warning, fonts disabled')
if not pygame.mixer: print('Warning, sound disabled')


# functions to create our resources
def load_image(name, colorkey=None):
    fullname = os.path.join('data', name)
    try:
        image = pygame.image.load(fullname)
    except pygame.error as message:
        print('Cannot load image:', fullname)
        raise SystemExit(message)
    image = image.convert()
    if colorkey is not None:
        if colorkey is -1:
            colorkey = image.get_at((0, 0))
        image.set_colorkey(colorkey, RLEACCEL)
    return image, image.get_rect()

class bullet(pygame.sprite.Sprite):

  def __init__(self):
        pygame.sprite.Sprite.__init__(self)  # call Sprite intializer
        self.image, self.rect = load_image('bullet.pngf', -1)
        screen = pygame.display.get_surface()
        self.area = screen.get_rect()
        self.rect.topleft = 10, 300
        self.move = 1
        self.x = 0
        self.y = 0


class Ship(pygame.sprite.Sprite):

    def __init__(self):
        pygame.sprite.Sprite.__init__(self)  # call Sprite intializer
        self.image, self.rect = load_image('fighter.gif', -1)
        screen = pygame.display.get_surface()
        self.area = screen.get_rect()
        self.rect.topleft = 10, 300
        self.move = 1
        self.dizzy = 0
        self.x = 0
        self.y = 0


    def update(self):
        self.image = pygame.transform.flip(self.image, 1, 0)
        newpos = self.rect.move((self.x, 0))
        self.rect = newpos

def main():
    """this function is called when the program starts.
       it initializes everything it needs, then runs in
       a loop until the function returns."""
    # Initialize Everything
    pygame.init()
    screen = pygame.display.set_mode((800, 600))
    pygame.display.set_caption('Space War')
    pygame.mouse.set_visible(1)

    # Create The Backgound
    background = pygame.Surface(screen.get_size())
    background = background.convert()
    background.fill((0,0,0))

    # Put Text On The Background, Centered
    if pygame.font:
        font = pygame.font.Font(None, 36)
        text = font.render("Space War", 1, (255, 255, 255))
        textpos = text.get_rect(centerx=background.get_width() / 2)
        background.blit(text, textpos)

    # Display The Background
    screen.blit(background, (0, 0))
    pygame.display.flip()

    # Prepare Game Objects
    clock = pygame.time.Clock()
    ship = Ship()
    allsprites = pygame.sprite.RenderPlain((ship))

    # Main Loop
    while 1:
        clock.tick(100)

        # Handle Input Events
        for event in pygame.event.get():
            if event.type == QUIT:
               return
            elif event.type == KEYDOWN:
                print (event.key)
                if event.key == 275:
                    ship.x += 1
                elif event.key == 276:
                    ship.x -= 1
            elif event.type == KEYUP:
                print (event.key)
                ship.x = ship.x

        allsprites.update()

        # Draw Everything
        screen.blit(background, (0, 0))
        allsprites.draw(screen)
        pygame.display.update()


# Game Over


# this calls the 'main' function when this script is executed
if __name__ == '__main__': main()
